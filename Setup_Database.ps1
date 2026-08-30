# POS Database Auto Setup Script for Client Devices (Offline)
param()

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
if (-not $ScriptDir) { $ScriptDir = Get-Location }

# Paths
$SqlFile = Join-Path $ScriptDir "Database\Seed_New_Data.sql"
if (-not (Test-Path $SqlFile)) {
    $SqlFile = Join-Path $ScriptDir "Database\Schema.sql"
}
$ConfigFile = Join-Path $ScriptDir "POS.exe.config"

Write-Host "==========================================================" -ForegroundColor Cyan
Write-Host "       POS System - Offline Database Setup Script         " -ForegroundColor Yellow
Write-Host "==========================================================" -ForegroundColor Cyan
Write-Host ""

if (-not (Test-Path $SqlFile)) {
    Write-Host "[ERROR] SQL file not found in Database folder!" -ForegroundColor Red
    Read-Host "Press Enter to exit..."
    exit 1
}

# Candidate SQL Server Instances
$CandidateInstances = @(
    "(localdb)\MSSQLLocalDB",
    ".\SQLEXPRESS",
    "localhost\SQLEXPRESS",
    "localhost",
    ".",
    "(local)"
)

# Start LocalDB if tool exists
if (Get-Command "sqllocaldb" -ErrorAction SilentlyContinue) {
    Write-Host "[INFO] Checking LocalDB service..." -ForegroundColor Gray
    try {
        & sqllocaldb create MSSQLLocalDB 2>$null | Out-Null
        & sqllocaldb start MSSQLLocalDB 2>$null | Out-Null
    } catch {}
}

$ConnectedServer = $null

foreach ($server in $CandidateInstances) {
    Write-Host "[INFO] Testing SQL connection to: $server ..." -ForegroundColor Gray
    $testConnStr = "Server=$server;Database=master;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=3;"
    $conn = New-Object System.Data.SqlClient.SqlConnection($testConnStr)
    try {
        $conn.Open()
        $conn.Close()
        $ConnectedServer = $server
        Write-Host "[OK] Connected to SQL Server: $server" -ForegroundColor Green
        break
    }
    catch {
        # continue to next candidate
    }
    finally {
        if ($conn.State -eq [System.Data.ConnectionState]::Open) { $conn.Close() }
    }
}

if (-not $ConnectedServer) {
    Write-Host ""
    Write-Host "[ERROR] No local SQL Server instance found!" -ForegroundColor Red
    Write-Host "Please ensure Microsoft SQL Server Express or SQL Server LocalDB is installed." -ForegroundColor Yellow
    Write-Host ""
    Read-Host "Press Enter to exit..."
    exit 1
}

Write-Host ""
Write-Host "[INFO] Reading database script: $SqlFile ..." -ForegroundColor Cyan
$sqlContent = [System.IO.File]::ReadAllText($SqlFile, [System.Text.Encoding]::UTF8)

# Split by GO commands
$batches = [System.Text.RegularExpressions.Regex]::Split($sqlContent, "(?im)^\s*GO\s*$")

Write-Host "[INFO] Creating database and tables..." -ForegroundColor Cyan

$masterConnStr = "Server=$ConnectedServer;Database=master;Integrated Security=True;TrustServerCertificate=True;"
$dbConn = New-Object System.Data.SqlClient.SqlConnection($masterConnStr)

try {
    $dbConn.Open()
    $cmd = $dbConn.CreateCommand()
    $cmd.CommandTimeout = 180

    foreach ($batch in $batches) {
        $trimmed = $batch.Trim()
        if (-not [string]::IsNullOrWhiteSpace($trimmed)) {
            $cmd.CommandText = $trimmed
            try {
                $cmd.ExecuteNonQuery() | Out-Null
            }
            catch {
                $msg = $_.Exception.Message
                if ($msg -notmatch "already exists" -and $msg -notmatch "There is already an object") {
                    Write-Host " [Warning] $msg" -ForegroundColor DarkYellow
                }
            }
        }
    }

    Write-Host "[OK] POS_DB Database and seed data created successfully!" -ForegroundColor Green
}
catch {
    Write-Host "[ERROR] Failed executing SQL script: $($_.Exception.Message)" -ForegroundColor Red
    Read-Host "Press Enter to exit..."
    exit 1
}
finally {
    if ($dbConn.State -eq [System.Data.ConnectionState]::Open) { $dbConn.Close() }
}

# Update POS.exe.config if needed
if (Test-Path $ConfigFile) {
    try {
        [xml]$configXml = Get-Content $ConfigFile
        $connNode = $configXml.SelectSingleNode("//connectionStrings/add[@name='POS_DB']")
        if ($connNode) {
            $newConnStr = "Data Source=$ConnectedServer;Initial Catalog=POS_DB;Integrated Security=True;TrustServerCertificate=True;"
            $connNode.SetAttribute("connectionString", $newConnStr)
            $configXml.Save($ConfigFile)
            Write-Host "[OK] Updated POS.exe.config with server: $ConnectedServer" -ForegroundColor Green
        }
    }
    catch {
        Write-Host "[!] Could not update POS.exe.config: $($_.Exception.Message)" -ForegroundColor Yellow
    }
}

Write-Host ""
Write-Host "==========================================================" -ForegroundColor Green
Write-Host "  Database Setup Completed Successfully!                  " -ForegroundColor Green
Write-Host "  Default Login:                                          " -ForegroundColor White
Write-Host "  - Username: admin                                       " -ForegroundColor Cyan
Write-Host "  - Password: admin123                                    " -ForegroundColor Cyan
Write-Host "==========================================================" -ForegroundColor Green
Write-Host ""

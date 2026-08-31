; -----------------------------------------------------------------------------
; سكريبت التثبيت الشامل المتكامل لنظام نقاط البيع (POS System)
; يشمل تلقائياً:
; 1. أيقونة سلة المشتريات الاحترافية (Shopping Cart Icon)
; 2. Microsoft Visual C++ 2015-2022 Redistributable (x64)
; 3. Microsoft .NET Framework 4.8.1
; 4. Microsoft SQL Server LocalDB Engine
; 5. ملفات البرنامج والخطوط (Cairo Fonts)
; 6. إنشاء وتهيئة قاعدة البيانات تلقائياً بحساب الأدمن النظيف
; -----------------------------------------------------------------------------

#define MyAppName "نظام إدارة المبيعات - POS"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "POS System"
#define MyAppExeName "POS.exe"
#define SourceDir "C:\Users\MAGDY\source\repos\POS"

[Setup]
AppId={{C8B25A62-8E3A-4F1D-894A-21E65892D5A1}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\POS System
DefaultGroupName={#MyAppName}
OutputDir={#SourceDir}\Output
OutputBaseFilename=POS_Setup_Full_v1.0
SetupIconFile={#SourceDir}\app_icon.ico
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64compatible

[Languages]
Name: "arabic"; MessagesFile: "compiler:Languages\Arabic.isl"
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
; --- حزم البرامج المساعدة (تُحذف تلقائياً بعد التثبيت لتوفير المساحة) ---
Source: "{#SourceDir}\Redist\VC_redist.x64.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall
Source: "{#SourceDir}\Redist\ndp481-web.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall
Source: "{#SourceDir}\Redist\SqlLocalDB.msi"; DestDir: "{tmp}"; Flags: deleteafterinstall

; --- ملفات البرنامج الأساسية والأيقونة ---
Source: "{#SourceDir}\app_icon.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}\bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceDir}\Database\*"; DestDir: "{app}\Database"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceDir}\fonts\*"; DestDir: "{app}\fonts"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#SourceDir}\Setup_Database.ps1"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\app_icon.ico"
Name: "{group}\إلغاء التثبيت"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon; IconFilename: "{app}\app_icon.ico"

[Run]
; 1. تثبيت حزمة Visual C++ إذا لم تكن موجودة
Filename: "{tmp}\VC_redist.x64.exe"; Parameters: "/install /quiet /norestart"; Flags: waituntilterminated; StatusMsg: "جاري تثبيت حزم Microsoft Visual C++ المطلوبة..."; Check: NeedsVCRedist

; 2. تثبيت .NET Framework 4.8.1 إذا لزم الأمر
Filename: "{tmp}\ndp481-web.exe"; Parameters: "/q /norestart"; Flags: waituntilterminated; StatusMsg: "جاري التحقق من وتثبيت Microsoft .NET Framework..."; Check: NeedsDotNet

; 3. تثبيت محرك قاعدة البيانات SQL LocalDB
Filename: "msiexec.exe"; Parameters: "/i ""{tmp}\SqlLocalDB.msi"" /qn /norestart IACCEPTSQLLOCALDBLICENSETERMS=YES"; Flags: waituntilterminated; StatusMsg: "جاري تثبيت محرك قاعدة البيانات (SQL Server LocalDB)..."; Check: NeedsSqlLocalDB

; 4. إنشاء قاعدة البيانات النظيفة وتهيئة حساب المدير
Filename: "powershell.exe"; Parameters: "-ExecutionPolicy Bypass -WindowStyle Hidden -File ""{app}\Setup_Database.ps1"""; Flags: runhidden waituntilterminated; StatusMsg: "جاري تهيئة قاعدة البيانات وضبط إعدادات النظام..."

; 5. تشغيل البرنامج عند إنهاء التثبيت
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
// التحقق من وجود Visual C++ 2015-2022 x64
function NeedsVCRedist: Boolean;
var
  Installed: Cardinal;
begin
  Result := True;
  if RegQueryDWordValue(HKLM64, 'SOFTWARE\Microsoft\VisualStudio\14.0\VC\Runtimes\x64', 'Installed', Installed) then
  begin
    if Installed = 1 then
      Result := False;
  end;
end;

// التحقق من وجود .NET Framework 4.8 أو أعلى
function NeedsDotNet: Boolean;
var
  Release: Cardinal;
begin
  Result := True;
  if RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', Release) then
  begin
    if Release >= 528040 then
      Result := False;
  end;
end;

// التحقق من وجود SQL LocalDB
function NeedsSqlLocalDB: Boolean;
var
  i: Integer;
  paths: array[0..6] of String;
begin
  paths[0] := ExpandConstant('{commonpf}\Microsoft SQL Server\170\Tools\Binn\SqlLocalDB.exe');
  paths[1] := ExpandConstant('{commonpf}\Microsoft SQL Server\160\Tools\Binn\SqlLocalDB.exe');
  paths[2] := ExpandConstant('{commonpf}\Microsoft SQL Server\150\Tools\Binn\SqlLocalDB.exe');
  paths[3] := ExpandConstant('{commonpf}\Microsoft SQL Server\140\Tools\Binn\SqlLocalDB.exe');
  paths[4] := ExpandConstant('{commonpf}\Microsoft SQL Server\130\Tools\Binn\SqlLocalDB.exe');
  paths[5] := ExpandConstant('{commonpf}\Microsoft SQL Server\120\Tools\Binn\SqlLocalDB.exe');
  paths[6] := ExpandConstant('{sys}\SqlLocalDB.exe');

  for i := 0 to 6 do
  begin
    if FileExists(paths[i]) then
    begin
      Result := False;
      Exit;
    end;
  end;
  Result := True;
end;

@echo off
chcp 65001 > nul
title POS - Database Setup
echo ========================================================
echo   جاري تهيئة وتثبيت قاعدة البيانات للنظام...
echo   Setting up POS Database...
echo ========================================================
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0Setup_Database.ps1"
pause

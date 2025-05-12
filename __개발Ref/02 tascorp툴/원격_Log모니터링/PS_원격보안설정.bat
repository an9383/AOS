@findstr/v "^@f.*&" "%~f0"|powershell -&goto:eof

Write-Host "Client PowerShell 보안설정 시작....."

Set-ExecutionPolicy Unrestricted -force

Set-Item WSMan:\localhost\Client\TrustedHosts -Value "*" -Force

Write-Host "Client PowerShell 보안설정 종료....."

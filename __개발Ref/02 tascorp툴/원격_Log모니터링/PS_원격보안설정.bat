@findstr/v "^@f.*&" "%~f0"|powershell -&goto:eof

Write-Host "Client PowerShell ���ȼ��� ����....."

Set-ExecutionPolicy Unrestricted -force

Set-Item WSMan:\localhost\Client\TrustedHosts -Value "*" -Force

Write-Host "Client PowerShell ���ȼ��� ����....."

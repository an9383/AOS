@findstr/v "^@f.*&" "%~f0"|powershell -&goto:eof

Invoke-Command -ComputerName 192.168.0.6 -Credential Administrator -ScriptBlock {D:\__websroot\HACCP_AOS\__logMonitor\logMonitor.ps1}
@findstr/v "^@f.*&" "%~f0"|powershell -&goto:eof
#$logPath = [IO.Path]::Combine($PSScriptRoot) + "Monitor"

# 공통설정
$logRoot = "C:\tascorp\Log\"
# 개별설정
# pjt 로그폴더명 설정
$pjtfolder = "HACCP_AOS"

$logPath = $logRoot + $pjtfolder + "\Monitor"

$latestFileName = Get-ChildItem $logPath | Sort Name -Descending | Select-Object -First 1


$logFile = [IO.Path]::Combine($logPath, $latestFileName)
Get-Content $logFile -Wait -Last 200

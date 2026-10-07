$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path -Parent $PSScriptRoot
$appRoot = Join-Path $projectRoot 'WebAppLottery'
$outputDir = Join-Path $PSScriptRoot 'bin'
New-Item -ItemType Directory -Path $outputDir -Force | Out-Null
$compilerPath = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
$compilerArgs = @('/nologo', ('/out:' + (Join-Path $outputDir 'ApiTests.exe')), '/r:System.Web.dll', '/r:System.Configuration.dll', '/r:System.Data.dll', '/r:System.Core.dll')
foreach ($dependency in @('System.Web.Mvc', 'EntityFramework', 'LotteryDAL', 'Newtonsoft.Json')) {
    $dependencyPath = Join-Path $appRoot ('bin\' + $dependency + '.dll')
    $compilerArgs += '/r:' + $dependencyPath
    Copy-Item -LiteralPath $dependencyPath -Destination $outputDir -Force
}
$compilerArgs += Join-Path $appRoot 'Controllers\LotteryApiController.cs'
$compilerArgs += Join-Path $appRoot 'Models\LotteryDraw.cs'
$compilerArgs += Join-Path $PSScriptRoot 'ApiTests.cs'
& $compilerPath @compilerArgs
if ($LASTEXITCODE -ne 0) { throw 'API test compilation failed.' }
& (Join-Path $outputDir 'ApiTests.exe')
if ($LASTEXITCODE -ne 0) { throw 'API tests failed.' }

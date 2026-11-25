# 构建插件并打包 .cipx
$ErrorActionPreference = 'Stop'
$out = Join-Path $PSScriptRoot '..\out'
if (Test-Path $out) { Remove-Item $out -Recurse -Force }
dotnet publish (Join-Path $PSScriptRoot '..\MusicBell.ClassIsland.csproj') -c Release -o $out --nologo
if ($LASTEXITCODE -ne 0) { throw '编译失败' }
Remove-Item (Join-Path $out '*.pdb') -Force -ErrorAction SilentlyContinue
$cipx = Join-Path $PSScriptRoot '..\MusicBell.ClassIsland.cipx'
if (Test-Path $cipx) { Remove-Item $cipx -Force }
Add-Type -AssemblyName System.IO.Compression.FileSystem
[System.IO.Compression.ZipFile]::CreateFromDirectory($out, $cipx, [System.IO.Compression.CompressionLevel]::Optimal, $false)
Write-Host ('打包完成：' + $cipx)

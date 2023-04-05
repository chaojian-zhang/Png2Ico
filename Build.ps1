$PrevPath = pwd

Write-Host "Publish for Github release."
cd $PSScriptRoot

$PublishFolder = "$PSScriptRoot\GithubBuild"

# Delete current data
Remove-Item $PublishFolder -Recurse -Force

# dotnet publish new
dotnet publish $PSScriptRoot\Png2Ico\Png2Ico.csproj --runtime win-x64 --self-contained false --output $PublishFolder /p:PublishSingleFile=true

# Create archive
$Date = Get-Date -Format yyyyMMdd
$ArchiveFolder = "$PublishFolder\Packages"
$ArchivePath = "$ArchiveFolder\Png2Ico_GithubBuild_Windows_x64_B$Date.zip"
New-Item -ItemType Directory -Force -Path $ArchiveFolder
Compress-Archive -Path $PublishFolder\*.* -DestinationPath $ArchivePath

# Validation
if (-Not (Test-Path (Join-Path $PublishFolder "Png2Ico.exe")))
{
    Write-Host "Build failed."
    Exit
}

cd $PrevPath
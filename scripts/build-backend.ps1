$ErrorActionPreference = "Stop"

$repositoryRoot = Split-Path -Parent $PSScriptRoot
$dotenvPath = Join-Path $repositoryRoot "src\backend\.env"
$solutionPath = Join-Path $repositoryRoot "src\backend\MediaBoomService.sln"

if (Test-Path -LiteralPath $dotenvPath)
{
    $dotenvValues = Get-Content -LiteralPath $dotenvPath -Raw | ConvertFrom-StringData
    $username = $dotenvValues.GITHUB_PACKAGES_USERNAME
    $token = $dotenvValues.GITHUB_PACKAGES_TOKEN

    if ([string]::IsNullOrWhiteSpace($username) -or [string]::IsNullOrWhiteSpace($token))
    {
        throw "The .env file must define GITHUB_PACKAGES_USERNAME and GITHUB_PACKAGES_TOKEN."
    }

    $credentials = "Username=$username;Password=$token;ValidAuthenticationTypes=Basic"
    [Environment]::SetEnvironmentVariable(
        "NuGetPackageSourceCredentials_github-frMks",
        $credentials,
        "Process")
}

dotnet build $solutionPath /property:GenerateFullPaths=true /consoleloggerparameters:NoSummary
exit $LASTEXITCODE

Write-Host "------------ Publishing application ------------------------"

& dotnet publish ./src/FinanceControl.Services.Users.Api -c Release -o ./bin/docker

Write-Host "------------ Publishing application completed --------------"

Write-Verbose "------------ Publishing application ------------------------"

& dotnet publish ./src/FinanceControl.Services.Users.Api -c Release -o ./bin/docker

Write-Verbose "------------ Publishing application completed --------------"

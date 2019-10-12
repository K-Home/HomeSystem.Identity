Write-Verbose "------------ Building docker image ------------------------"

& docker build -t financecontrol.services.users:local .

Write-Verbose "------------ Building docker image completed --------------"

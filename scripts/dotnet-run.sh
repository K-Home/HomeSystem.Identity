#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/FinanceControl.Services.Users.Api
dotnet run --no-restore
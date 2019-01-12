#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/HomeSystem.Services.Identity
dotnet run --no-restore
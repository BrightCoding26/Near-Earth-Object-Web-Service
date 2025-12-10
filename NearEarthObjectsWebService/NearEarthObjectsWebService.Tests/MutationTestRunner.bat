@echo off

dotnet build
dotnet tool install -g dotnet-stryker || dotnet tool update -g dotnet-stryker
dotnet stryker --open-report
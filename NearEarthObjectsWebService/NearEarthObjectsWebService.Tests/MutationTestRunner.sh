#!/bin/bash

set -e

dotnet build

if ! dotnet tool list -g | grep -q 'dotnet-stryker'; then
  dotnet tool install -g dotnet-stryker
else
  dotnet tool update -g dotnet-stryker
fi

dotnet stryker --open-report
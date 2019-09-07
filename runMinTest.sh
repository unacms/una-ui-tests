#!/bin/bash

dotnet test --filter EmptyEmailField_ShowsErrorMessage --logger "trx;LogFileName=result.trx"
./runTests.sh EmptyEmailField_ShowsErrorMessage
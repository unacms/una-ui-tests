FROM microsoft/dotnet:2.2-sdk AS build-env

COPY / ./tests

RUN sed -i 's/let browserUrl = /let browserUrl = "http:\/\/testbrowser:4444\/wd\/hub\/" \/\/ /g' tests/Program.fs && \
    sed -i 's/.\/results.xml/\/tests\/TestResults\/results.xml/g' tests/Program.fs && \
    dotnet build tests/una-ui-tests.fsproj

VOLUME /tests/TestResults

ENTRYPOINT ["dotnet", "tests/bin/Debug/netcoreapp2.2/una-ui-tests.dll"]

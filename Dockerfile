FROM microsoft/dotnet:2.2-sdk AS build-env

RUN apt-get update && apt-get install -y libc6-dev libgdiplus

COPY / ./tests

RUN sed -i 's/let browserUrl = /let browserUrl = "http:\/\/testbrowser:4444\/wd\/hub\/" \/\/ /g' tests/Program.fs && \
    sed -i 's/.\/TestResult.xml/\/tests\/TestResults\/results.xml/g' tests/Program.fs && \
    sed -i 's/".\/TestResults"/"\/tests\/TestResults"/g' tests/Program.fs && \
    sed -i 's/configuration.failIfAnyWipTests <- false/configuration.failIfAnyWipTests <- true/g' tests/Program.fs
    

RUN dotnet build tests/una-ui-tests.fsproj

VOLUME /tests/TestResults

ENTRYPOINT ["dotnet", "tests/bin/Debug/netcoreapp2.2/una-ui-tests.dll"]

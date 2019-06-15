FROM microsoft/dotnet:2.2-sdk AS build-env

RUN apt-get update && apt-get install -y libc6-dev libgdiplus

COPY / ./tests

RUN sed -i 's/let browserUrl = /let browserUrl = "http:\/\/hub:4444\/wd\/hub\/" \/\/ /g' tests/Tests/TestsSetup.fs && \
    sed -i 's/setDriverFactory /setDriverFactory createRemoteDriver \/\/ /g' tests/Tests/TestsSetup.fs 
   

VOLUME /tests/TestResults

ENTRYPOINT ["dotnet", "test","--logger","trx;LogFileName=result.trx","/tests/una-ui-tests.fsproj","--filter"]
CMD ["Name!=NotExistingTestNameToRunAllTests"]
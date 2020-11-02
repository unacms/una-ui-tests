#!/bin/bash

testfilter=$1

echo "Make sure you have set the password in environment variable adminexamplepassword. Current value is $adminexamplepassword"

mkdir ./TestResults -p

if [ -z "$testfilter" ];
then
    docker-compose up --abort-on-container-exit --build 
else 
    # this else block is pure for running locally to test just some tests to avoid waiting all tests execution

    # did not find the way to specify arguments for entrypoint so below use docker-compose run
    # als did not find the way in docker-compose run to specify --build so this is why I do
    # build here and tag is required as docker-compose run will use an image tagged una-ui-tests_test
    docker build -t una-ui-tests_test .

    # In the line below for the service "test" we override additional argument which is in CMD after entrypoint
    # NOTE there're no options --abort-on-container-exit --build
    docker-compose run test $testfilter 
fi

# ToDo make proper clean up as this doesn't clean completely. 
# This also `docker image prune -f -a` doesn't clean, so it might be better to walk through the list `docker image ls` and clean everything except microsoft/dotnet:2.2-sdk & selenium/standalone-chrome
docker image prune -f

# dotnet test --filter CreateNewOrganizationProfile
# dotnet test --filter CreateNewPersonalProfileTests
# dotnet test --filter EditOrganizationProfile
# dotnet test --filter EditPersonalProfileTests
# dotnet test --filter LoginTests
# dotnet test --filter MainMenuPersonalProfileTests
# dotnet test --filter PostToFeedTests
# dotnet test --filter ProfileToolbarTests
# dotnet test --filter
# dotnet test --filter
# dotnet test --filter
# dotnet test --filter

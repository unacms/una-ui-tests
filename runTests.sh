#!/bin/bash

mkdir ./TestResults -p

docker-compose up --abort-on-container-exit --build

# ToDo make proper clean up as this doesn't clean completely. 
# This also `docker image prune -f -a` doesn't clean, so it might be better to walk through the list `docker image ls` and clean everything except microsoft/dotnet:2.2-sdk & selenium/standalone-chrome
docker image prune -f

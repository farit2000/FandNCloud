#!/usr/bin/env bash
cd src
dotnet publish ./FandNCloud.Api -c Release -o ./bin/Docker
dotnet publish ./FandNCloud.Services.BasketActivities -c Release -o ./bin/Docker
dotnet publish ./FandNCloud.Services.Identity -c Release -o ./bin/Docker
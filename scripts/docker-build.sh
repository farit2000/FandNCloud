#!/usr/bin/env bash
cd src
docker build -f ./FandNCloud.Api/Dockerfile -t fandncloud.api ./FandNCloud.Api
docker build -f ./FandNCloud.Services.BasketActivities/Dockerfile -t fandncloud.services.basketactivities ./FandNCloud.Services.BasketActivities
docker build -f ./FandNCloud.Services.Identity/Dockerfile -t fandncloud.services.identity ./FandNCloud.Services.Identity
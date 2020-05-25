FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5100
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "FandNCloud.Services.Identity.dll"]

#FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
#WORKDIR /app
#
## Copy csproj and restore as distinct layers
#COPY *.csproj ./
#RUN dotnet restore
#
## Copy everything else and build
#COPY . ./
#RUN dotnet publish -c Release -o ./bin/Docker
#
## Build runtime image
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
#WORKDIR /app
#COPY ./bin/Docker .
#ENV ASPNETCORE_URLS http://*:5100
#ENV ASPNETCORE_ENVIRONMENT docker
#ENTRYPOINT ["dotnet", "FandNCloud.Services.Identity.dll"]
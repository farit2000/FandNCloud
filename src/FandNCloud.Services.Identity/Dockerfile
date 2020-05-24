FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5100
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "FandNCloud.Services.Identity.dll"]
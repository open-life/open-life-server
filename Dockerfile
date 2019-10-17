FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
COPY . ./
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
COPY --from=build-env open-life-server/bin/Release/netcoreapp2.2/publish/ app/
ENTRYPOINT ["dotnet", "app/open-life-server.dll"]
EXPOSE 80
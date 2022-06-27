FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /app

COPY ["/AuthService/", "/AuthService/"]
RUN dotnet publish /AuthService/AuthService/AuthService.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "LT.DigitalOffice.AuthService.dll"]
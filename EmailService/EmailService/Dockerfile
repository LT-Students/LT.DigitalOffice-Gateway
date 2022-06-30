FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /app

COPY ["/EmailService/", "/EmailService/"]
RUN dotnet publish /EmailService/EmailService/EmailService.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "LT.DigitalOffice.EmailService.dll"]
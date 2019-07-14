FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2.301 AS build
WORKDIR /src
COPY . .
RUN dotnet tool install --global paket
ENV PATH="/root/.dotnet/tools:${PATH}"

WORKDIR /src/src/Catalog.Api
RUN paket restore
RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet build -c Release -o /app

FROM build as test
WORKDIR /src/tests/Catalog.Tests
RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet test

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]
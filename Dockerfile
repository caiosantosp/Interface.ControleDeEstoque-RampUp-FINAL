#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["/ControleDeEstoqueProduto/ControleDeEstoqueProduto.csproj", "ControleDeEstoqueProduto/"]
RUN dotnet restore "ControleDeEstoqueProduto/ControleDeEstoqueProduto.csproj"
COPY . .
WORKDIR "/src/ControleDeEstoqueProduto"
RUN dotnet build "ControleDeEstoqueProduto.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControleDeEstoqueProduto.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS="http://*$PORT" dotnet ControleDeEstoqueProduto.dll
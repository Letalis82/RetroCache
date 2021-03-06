#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["RetroCacheApi/RetroCacheApi.csproj", "RetroCacheApi/"]
COPY ["RetroCache.Shared/RetroCache.Shared.csproj", "RetroCache.Shared/"]
RUN dotnet restore "RetroCacheApi/RetroCacheApi.csproj"
COPY . .
WORKDIR "/src/RetroCacheApi"
RUN dotnet build "RetroCacheApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RetroCacheApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RetroCacheApi.dll"]

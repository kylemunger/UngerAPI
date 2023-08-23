#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UngerApi.Minimal.csproj", "."]
RUN dotnet restore "UngerApi.Minimal.csproj"
COPY . .
RUN dotnet build "UngerApi.Minimal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UngerApi.Minimal.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UngerApi.Minimal.dll"]
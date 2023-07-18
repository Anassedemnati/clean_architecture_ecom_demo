#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Basket/Basket.Api/Basket.Api.csproj", "Basket/Basket.Api/"]
COPY ["Basket/Basket.Infrastructure/Basket.Infrastructure.csproj", "Basket/Basket.Infrastructure/"]
COPY ["Basket/Basket.Application/Basket.Application.csproj", "Basket/Basket.Application/"]
COPY ["Basket/Basket.Domain/Basket.Domain.csproj", "Basket/Basket.Domain/"]
RUN dotnet restore "Basket/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/Basket/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]
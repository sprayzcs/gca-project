#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Backend/Services/CartService/CartService.csproj", "Backend/Services/CartService/"]
COPY ["Backend/Shared/Shared.csproj", "Backend/Shared/"]
RUN dotnet restore "Backend/Services/CartService/CartService.csproj"
COPY . .
WORKDIR "/src/Backend/Services/CartService"
RUN dotnet build "CartService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CartService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CartService.dll"]
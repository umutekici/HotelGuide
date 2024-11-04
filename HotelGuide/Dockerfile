FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HotelMicroService/HotelMicroService.csproj", "HotelMicroService/"]
RUN dotnet restore "HotelMicroService/HotelMicroService.csproj"
COPY . .
WORKDIR "/src/HotelMicroService"
RUN dotnet build "HotelMicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotelMicroService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelMicroService.dll"]

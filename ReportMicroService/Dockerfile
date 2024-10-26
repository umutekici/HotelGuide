FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ReportMicroService/ReportMicroService.csproj", "ReportMicroService/"]
RUN dotnet restore "ReportMicroService/ReportMicroService.csproj"
COPY . .
WORKDIR "/src/ReportMicroService"
RUN dotnet build "ReportMicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportMicroService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportMicroService.dll"]

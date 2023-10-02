FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TesteTecnicoIdealSoft.API/TesteTecnicoIdealSoft.API.csproj", "TesteTecnicoIdealSoft.API/"]
RUN dotnet restore "TesteTecnicoIdealSoft.API/TesteTecnicoIdealSoft.API.csproj"
COPY . .
WORKDIR "/src/TesteTecnicoIdealSoft.API"
RUN dotnet build "TesteTecnicoIdealSoft.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TesteTecnicoIdealSoft.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TesteTecnicoIdealSoft.API.dll"]
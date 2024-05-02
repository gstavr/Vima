FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Vimachem.csproj", "."]
RUN dotnet restore "./Vimachem.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Vimachem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vimachem.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vimachem.dll"]

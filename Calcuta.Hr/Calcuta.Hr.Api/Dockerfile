FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Calcuta.Hr.App.sln ./
COPY Calcuta.Hr.Api/ ./Calcuta.Hr.Api/
RUN dotnet restore ./Calcuta.Hr.Api/Calcuta.Hr.Api.csproj
RUN dotnet publish ./Calcuta.Hr.Api/Calcuta.Hr.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Calcuta.Hr.Api.dll"]

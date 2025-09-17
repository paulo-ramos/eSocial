
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Calcuta.Hr.App.sln ./
COPY Calcuta.Hr.App/Calcuta.Hr.ConsoleApp/ ./Calcuta.Hr.ConsoleApp/
COPY Calcuta.Hr.Domain/ ./Calcuta.Hr.Domain/
RUN dotnet restore ./Calcuta.Hr.ConsoleApp/Calcuta.Hr.ConsoleApp.csproj
RUN dotnet publish ./Calcuta.Hr.ConsoleApp/Calcuta.Hr.ConsoleApp.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Calcuta.Hr.ConsoleApp.dll"]

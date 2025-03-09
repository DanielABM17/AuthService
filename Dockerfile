
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY AuthService/AuthService.csproj AuthService/
RUN dotnet restore "AuthService/AuthService.csproj"


COPY . .
WORKDIR "/src/AuthService"
RUN dotnet publish "AuthService.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migration
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
RUN dotnet ef database update


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080


ENV ASPNETCORE_ENVIRONMENT=Production


ENTRYPOINT [ "dotnet", "AuthService.dll" ]

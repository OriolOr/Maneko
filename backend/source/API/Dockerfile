FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 5000


# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OriolOr.Maneko.API.csproj", "./"]
RUN dotnet restore "OriolOr.Maneko.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "OriolOr.Maneko.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OriolOr.Maneko.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OriolOr.Maneko.API.dll"]

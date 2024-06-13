FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["KafkaProducer/KafkaProducer.csproj", "KafkaProducer/"]
RUN dotnet restore "KafkaProducer\KafkaProducer.csproj"
COPY . .
WORKDIR "/src/KafkaProducer"
RUN dotnet build "KafkaProducer.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "KafkaProducer.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaProducer.dll"]

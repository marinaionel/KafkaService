FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["KafkaConsumer/KafkaConsumer.csproj", "KafkaConsumer/"]
RUN dotnet restore "KafkaConsumer\KafkaConsumer.csproj"
COPY . .
WORKDIR "/src/KafkaConsumer"
RUN dotnet build "KafkaConsumer.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "KafkaConsumer.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaConsumer.dll"]

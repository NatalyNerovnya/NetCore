FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["NetCoreMentoring/NetCoreMentoring.csproj", "NetCoreMentoring/"]
COPY ["NetCoreProject.Common/NetCoreProject.Common.csproj", "NetCoreProject.Common/"]
COPY ["NetCoreProject.Domain/NetCoreProject.Domain.csproj", "NetCoreProject.Domain/"]
COPY ["NetCoreProject.Data.Common/NetCoreProject.Data.Common.csproj", "NetCoreProject.Data.Common/"]
COPY ["NetCoreProject.Data/NetCoreProject.Data.csproj", "NetCoreProject.Data/"]
RUN dotnet restore "NetCoreMentoring/NetCoreMentoring.csproj"
COPY . .
WORKDIR "/src/NetCoreMentoring"
RUN dotnet build "NetCoreMentoring.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NetCoreMentoring.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NetCoreMentoring.dll"]
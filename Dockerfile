#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ControlViajes/ControlViajes.csproj", "ControlViajes/"]
RUN dotnet restore "ControlViajes/ControlViajes.csproj"
COPY . .
WORKDIR "/src/ControlViajes"
RUN dotnet build "ControlViajes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControlViajes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControlViajes.dll"]
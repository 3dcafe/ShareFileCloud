#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ShareFileCloud/ShareFileCloud.csproj", "ShareFileCloud/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["DTO/DTO.csproj", "DTO/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["Entitles/Entitles.csproj", "Entitles/"]
RUN dotnet restore "ShareFileCloud/ShareFileCloud.csproj"
COPY . .
WORKDIR "/src/ShareFileCloud"
RUN dotnet build "ShareFileCloud.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShareFileCloud.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShareFileCloud.dll"]
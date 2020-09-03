#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Twajd-Back-End.Api/Twajd-Back-End.Api.csproj", "Twajd-Back-End.Api/"]
COPY ["Twajd-Back-End.Root/Twajd-Back-End.Root.csproj", "Twajd-Back-End.Root/"]
COPY ["Twajd-Back-End.Business/Twajd-Back-End.Business.csproj", "Twajd-Back-End.Business/"]
COPY ["Twajd-Back-End.Core/Twajd-Back-End.Core.csproj", "Twajd-Back-End.Core/"]
COPY ["Twajd-Back-End.DataAccess/Twajd-Back-End.DataAccess.csproj", "Twajd-Back-End.DataAccess/"]
RUN dotnet restore "Twajd-Back-End.Api/Twajd-Back-End.Api.csproj"
COPY . .
WORKDIR "/src/Twajd-Back-End.Api"
RUN dotnet build "Twajd-Back-End.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Twajd-Back-End.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Twajd-Back-End.Api.dll"]
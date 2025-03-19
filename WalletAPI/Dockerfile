# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
# FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
# ARG TARGETARCH
# COPY . /source
# WORKDIR /source
# RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
#     dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

# FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
# WORKDIR /app
# COPY --from=build /app .
# ENTRYPOINT ["dotnet", "WalletAPI.dll"]


#This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./WalletAPI/WalletAPI.csproj", "WalletAPI/"]
RUN dotnet restore "./WalletAPI/WalletAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./WalletAPI.csproj" -c Release -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
RUN dotnet publish "./WalletAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WalletAPI.dll"]
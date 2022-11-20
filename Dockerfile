FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base

RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev \
     && rm -rf /var/lib/apt/lists/*

WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0.100-rc.1-bullseye-slim AS build

WORKDIR /src
COPY ["/Application_Layer/Application_Layer.csproj", "facturacion_api/Application_Layer/"]
COPY ["/Domain_Layer/Domain_Layer.csproj", "facturacion_api/Domain_Layer/"]
COPY ["/Infrastructure_Layer/Infrastructure_Layer.csproj", "facturacion_api/Infrastructure_Layer/"]

COPY . .
RUN dotnet restore
WORKDIR "/src/facturacion_api"
COPY . .

RUN dotnet build "Application_Layer/Application_Layer.csproj" -c Release -o /app/build
RUN dotnet build "Domain_Layer/Domain_Layer.csproj" -c Release -o /app/build
RUN dotnet build "Infrastructure_Layer/Infrastructure_Layer.csproj" -c Release -o /app/build
FROM build AS publish

RUN dotnet publish -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#ENV IP_SERVER='192.168.0.x'
#ENV DATABASE_NAME_DB='IMPORTCRMDB'
#ENV HOST_DB='192.168.0.x'
#ENV ENGINE_DB='sqlserver'
#ENV USERNAME_DB='sistemas'
#ENV PASSWORD_DB='xxxx'
#ENV ENDPOINT_GRAYLOG='http://192.168.0.77:12203/gelf'
ENV TZ 'America/La_Paz'
RUN echo $TZ > /etc/timezone && \
    apt-get update && apt-get install -y tzdata && \
    rm /etc/localtime && \
    ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && \
    dpkg-reconfigure -f noninteractive tzdata && \
    apt-get clean

EXPOSE 1007

ENTRYPOINT ["dotnet", "Application_Layer.dll"]

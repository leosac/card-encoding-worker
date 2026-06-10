#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
FROM ubuntu:26.04 AS base
EXPOSE 5100

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CredentialProvisioning.Encoding.Worker.Server/CredentialProvisioning.Encoding.Worker.Server.csproj", "CredentialProvisioning.Encoding.Worker.Server/"]
RUN dotnet restore "CredentialProvisioning.Encoding.Worker.Server/CredentialProvisioning.Encoding.Worker.Server.csproj"
COPY . .
WORKDIR "/src/CredentialProvisioning.Encoding.Worker.Server"

FROM build AS publish
RUN dotnet publish "CredentialProvisioning.Encoding.Worker.Server.csproj" -c Release -o /app/publish -r linux-x64 /p:UseAppHost=true --self-contained true

FROM base AS final
VOLUME /data
RUN apt update && apt install -y --no-install-recommends libpcsclite1 libboost-filesystem1.88.0 libboost-system1.88.0 && \
    apt-get clean && rm -rf /var/lib/apt/lists/*
WORKDIR /app
COPY --from=publish /app/publish .
RUN chmod +x CredentialProvisioning.Encoding.Worker.Server
COPY "liblogicalaccess.config" .
#RUN echo /app/runtimes/linux-x64/native>/etc/ld.so.conf.d/lla.conf & ldconfig
ENTRYPOINT ["/app/CredentialProvisioning.Encoding.Worker.Server", "--run"]
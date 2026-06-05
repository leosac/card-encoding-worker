#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
FROM ubuntu:26.04 AS base
RUN apt-get update && \
    apt-get install -y wget ca-certificates libicu78 tzdata && \
    wget https://dot.net/v1/dotnet-install.sh && \
    chmod +x dotnet-install.sh && \
    ./dotnet-install.sh --channel 8.0 --install-dir /usr/share/dotnet
ENV PATH="/usr/share/dotnet:$PATH"
WORKDIR /app
EXPOSE 5100

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CredentialProvisioning.Encoding.Worker.Server/CredentialProvisioning.Encoding.Worker.Server.csproj", "CredentialProvisioning.Encoding.Worker.Server/"]
RUN dotnet restore "CredentialProvisioning.Encoding.Worker.Server/CredentialProvisioning.Encoding.Worker.Server.csproj"
COPY . .
WORKDIR "/src/CredentialProvisioning.Encoding.Worker.Server"
RUN dotnet build "CredentialProvisioning.Encoding.Worker.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CredentialProvisioning.Encoding.Worker.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
VOLUME /data
RUN apt update && apt install -y libpcsclite1 libboost1.88-dev libboost-filesystem1.88-dev
WORKDIR /app
COPY --from=publish /app/publish .
COPY "liblogicalaccess.config" .
#RUN echo /app/publish/runtimes/linux-x64/native>/etc/ld.so.conf.d/lla.conf & ldconfig
ENTRYPOINT ["dotnet", "CredentialProvisioning.Encoding.Worker.Server.dll", "--run"]
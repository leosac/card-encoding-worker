FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

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
WORKDIR /app
COPY --from=publish /app/publish .
RUN echo /app/publish/runtimes/linux-x64/native>/etc/ld.so.conf.d/lla.conf & ldconfig
ENTRYPOINT ["dotnet", "CredentialProvisioning.Encoding.Worker.Server.dll", "--run"]
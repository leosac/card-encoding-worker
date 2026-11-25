# Card Encoding Worker [![Build Status](https://github.com/leosac/card-encoding-worker/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/leosac/card-encoding-worker/actions/workflows/build.yml)
An autonomous (ASP.NET Core) server to generate APDU commands for RFID cards (MIFARE DESFire, MIFARE Ultralight C, ...) following customly defined templates.

It can be used as a standalone microservice and is maintained/distributed as a core component of [Leosac Credential Provisioning](https://leosac.com/credential-provisioning/) solution.

# Run

## From source
 * Install ASP.NET Core Runtime 10.
 * `git clone https://github.com/leosac/card-encoding-worker.git`
 * Build the full solution CredentialProvisioning.Encoding.sln
 * Run CredentialProvisioning.Encoding.Worker.Server project

## With Docker
 * Install [Docker](https://docs.docker.com/engine/install/)
 * `docker pull leosac/leosac-card-encoding-worker:snapshot`
 * Create local directory /var/local/lcew/repo, if permanent caching will be enabled later on
 * Create local file /var/local/lcew/[appsettings.Production.json](https://raw.githubusercontent.com/leosac/card-encoding-worker/main/CredentialProvisioning.Encoding.Worker.Server/appsettings.Production.json)
 * `docker create --name leosac-cew --init -p 80:5100 -v /var/local/lcew/repo:/data/repository /var/local/lcew/appsettings.Production.json:/app/appsettings.Production.json leosac/leosac-card-encoding-worker:snapshot`
 * `docker start leosac-cew`

## Using MSI package (Windows only)
 * Install latest MSI package
 * Start Leosac.EncodingWorker service (Leosac Credential Provisioning Encoding Worker Service)

# Configuration
Configuration is done through appsettings.json file and environment specific appsettings.Production.json file which would override settings if defined.

# Use
By default, the server can be reached on http://localhost:5100/.
The REST API is documented with an embedded Swagger UI on http://localhost:5100/swagger/.

CORS allows requests from any origin by default when `Cors:Origins` is null or empty. To restrict origins, set `Cors:Origins` in appsettings to a list of origins, for example `"Cors": { "Origins": ["https://example.com"] }`. Origins must not include a trailing slash. Any method and headers are allowed; credentialed cross-origin requests (such as requests using cookies) are not supported. Restart the server after changing the CORS settings. CORS is not an authentication mechanism; configure an API key to protect the API.

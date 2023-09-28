<!-- ABOUT THE PROJECT -->
# YARP: Yet Another Reverse Proxy

YARP is built on .NET using the infrastructure from ASP.NET and .NET (.NET 6 and newer). The key differentiator for YARP is that it's been designed to be easily customized and tweaked via .NET code to match the specific needs of each deployment scenario.

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple example steps.

### Prerequisites

Things you need to use the software and how to install them.
* [Visual Studio / Visual Studio Code](https://visualstudio.microsoft.com/)
* [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet)
* [Docker](https://www.docker.com/)

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/gitViwe/yarp.git
   ```
2. Generate certificate. [Starting a container with https support using docker compose](https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-5.0#starting-a-container-with-https-support-using-docker-compose)
   ```
   dotnet dev-certs https -ep .aspnet\https\aspnetapp.pfx -p password!
   dotnet dev-certs https --trust
   ```
3. Run via Docker
   ```
   docker compose up --build -d
   ```

OpenTelemetry integration with Jeager UI: [localhost:16686](http://localhost:16686)
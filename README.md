# Weather Forecast API with OpenTelemetry

A simple .NET Web API that provides weather forecasts and includes OpenTelemetry integration with Honeycomb.

## Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Honeycomb API Key](https://ui.honeycomb.io/signup)

## Installation

1. Clone the repository:

```bash
git clone https://github.com/brianlangbecker/MigrationDotNet.git
cd MigrationDotNet
```

2. Navigate to the project directory:

```bash
cd MyDotNetWebApplication
```

3. Install dependencies:

```bash
dotnet restore
```

## Configuration

1. Set your Honeycomb API key as an environment variable:

For macOS/Linux:

```bash
export HONEYCOMB_API_KEY=your_api_key_here
```

For Windows:

```cmd
set HONEYCOMB_API_KEY=your_api_key_here
```

## Running the Application

1. Start the application:

```bash
dotnet run
```

The application will start and listen on:

- HTTP: http://localhost:6001
- HTTPS: https://localhost:6002

## Testing the API

You can test the Weather Forecast endpoint using curl:

```bash
curl http://localhost:6001/weatherforecast
```

Or using your web browser by navigating to:

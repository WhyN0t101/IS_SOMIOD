# SOMIOD - System Integration Service

A RESTful middleware service for integrating IoT applications using MQTT messaging protocol. This system provides a unified API for managing multiple IoT devices and applications through HTTP requests.

## Overview

SOMIOD (System Of Middleware for Internet Of Devices) is a C# .NET-based integration platform that enables seamless communication between various IoT applications. The project includes a REST API middleware service and several IoT client applications demonstrating real-world use cases.

## Features

- RESTful API for easy service integration
- MQTT messaging protocol support for real-time communication
- Multiple IoT application examples (Gate, Sprinkler, Light, Remote)
- Swagger documentation for API exploration
- Modular architecture for easy extension

## Technology Stack

- **C# .NET Framework** - Core development platform
- **ASP.NET Web API** - REST API framework
- **MQTT/Mosquitto** - Message broker for IoT communication
- **RestSharp** - HTTP client library
- **Swagger** - API documentation and testing
- **Visual Studio 2022** - IDE

## Prerequisites

Before you begin, ensure you have the following installed:

- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later
- [Mosquitto MQTT Broker](https://mosquitto.org/download/)
- [.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework) (version as specified in project)

## Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/WhyN0t101/IS_SOMIOD.git
   cd IS_SOMIOD
   ```

2. **Open the solution in Visual Studio**
   - Open `SOMIOD_IS.sln` in Visual Studio 2022

3. **Restore NuGet packages**
   - Right-click on the solution in Solution Explorer
   - Select "Restore NuGet Packages"
   - Or use: `Tools > NuGet Package Manager > Restore`

4. **Configure Mosquitto**
   - Install and start Mosquitto MQTT broker
   - Default configuration should work for local development
   - Ensure the broker is running on `localhost:1883`

5. **Run the Middleware**
   - Set `Middleware` as the startup project
   - Press F5 or click "Start" in Visual Studio
   - The API will be available at `http://localhost:52885`

6. **Access Swagger Documentation**
   - Navigate to `http://localhost:52885/swagger`
   - Explore and test API endpoints interactively

## Project Structure

The solution contains the following projects:

- **Middleware** - Main REST API service for IoT integration
- **Gate** - IoT application for gate control (open/close)
- **LightA** - IoT application for light management
- **Sprinkler** - IoT application for sprinkler system control
- **Remote** - Remote control application for managing devices
- **TestApplication** - Testing utilities and examples

## Usage

### Starting the System

1. **Start Mosquitto MQTT Broker**
   ```bash
   mosquitto -v
   ```

2. **Run the Middleware**
   - Start the Middleware project from Visual Studio
   - Verify it's running at `http://localhost:52885`

3. **Run IoT Applications**
   - Start any of the client applications (Gate, LightA, Sprinkler, Remote)
   - Applications will connect to the middleware automatically

### API Endpoints

The REST API provides endpoints for:
- Creating and managing applications
- Subscribing to data streams
- Publishing device data
- Querying device status

**Explore the complete API documentation at:** `http://localhost:52885/swagger`

### Example: Using the REST API

```csharp
// Example using RestSharp
var client = new RestClient("http://localhost:52885");
var request = new RestRequest("/api/applications", Method.POST);
request.AddJsonBody(new { name = "MyIoTApp" });
var response = client.Execute(request);
```

## IoT Applications

### Gate Application
Controls a gate system with open/close functionality. Demonstrates binary state management.

### LightA Application  
Manages lighting systems. Demonstrates state control and monitoring.

### Sprinkler Application
Controls sprinkler systems for irrigation. Demonstrates scheduled operations and state management.

### Remote Application
Unified remote control for managing multiple IoT devices through the middleware.

## Configuration

### Middleware Configuration
Edit `Web.config` in the Middleware project to configure:
- MQTT broker connection
- API endpoints
- Database connections (if applicable)

### Client Configuration
Each IoT application has an `App.config` file for:
- Middleware URL
- MQTT topics
- Application-specific settings

## Testing

The `TestApplication` project provides testing utilities and examples for:
- API endpoint testing
- MQTT message validation
- Integration testing scenarios

To run tests:
1. Set `TestApplication` as startup project
2. Press F5 to run

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the GNU General Public License - see the [LICENSE.txt](LICENSE.txt) file for details.


## Acknowledgments

- Politécnico de Leiria - System Integration Course
- Built with ASP.NET Web API
- MQTT messaging protocol
- Mosquitto message broker

---

**Note**: This project was developed as part of a System Integration course at Politécnico de Leiria. It demonstrates RESTful API design, MQTT messaging, and IoT application integration patterns.

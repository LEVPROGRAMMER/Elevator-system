# Project Structure

This project consists of two main directories:

- **backend**: Contains the ASP.NET Core project for the server-side API.

- **frontend**: Contains the React project for the client-side application.

## Technology Choices

- **Backend:** ASP.NET Core was chosen for its robust API capabilities and ease of integration with SQL Server.
- **Frontend:** React.js was selected for its component-based architecture, allowing for a dynamic user interface.
- **Database:** SQL Server is used for its reliability and support for complex queries.
- **Real-time** Communication: SignalR is implemented for real-time updates between the server and client.

# Elevator Management System Backend

This project simulates and manages elevators in a building, including handling elevator calls, managing elevator states, and providing real-time updates to clients using SignalR.

## Main Features

- **Automatic Elevator Call Handling**  
  Incoming elevator calls are automatically assigned to available elevators.

- **Elevator State Management**  
  Elevators move between floors according to their target queue, updating their current floor, door status, and operational status.

- **Real-Time Updates**  
  Elevator status updates are sent to all connected clients in real time via SignalR.

- **RESTful API**  
  Exposes endpoints for managing elevators, elevator calls, users, and more.

## Project Structure

- `BL\BlImplementation` - Business logic for elevators and calls.
- `BL\Bo` - Business objects such as `BLElevator`, `BLElevatorCalls`, etc.
- `Dal\Do` - Data objects and data access interfaces.
- `Server` - ASP.NET Core server, including configuration, SignalR, and API controllers.

## Key Files

- `ElevatorBackgroundService.cs`  
  Background service that runs the main elevator and call management loop.

- `ElevatorHub.cs`  
  SignalR hub for real-time elevator status updates.

- `BLElevatorService.cs`, `BLElevatorCallService.cs`  
  Business logic services for managing elevators and calls.

- `appsettings.json`  
  General configuration, including the background service interval.

## Getting Started

1. **Clone the repository**

2. **Configure the database**  
   Ensure your database connection settings in `appsettings.json` are correct.

3. **Run the project**
   - Open the solution in Visual Studio 2022.
   - Set the `Server` project as the startup project.
   - Press F5 to run.

4. **Test the API**  
   Use Postman or the provided `Server.http` file to test API endpoints.

## Technologies

- C# 12, .NET 8
- ASP.NET Core Web API
- SignalR
- Entity Framework Core

# Elevator System FrontEnd

This project is a web-based elevator system simulator. It provides a user interface to visualize and interact with elevators in a building, allowing users to request elevators to different floors and observe their movement in real time.

## Features
- Visual representation of a building with multiple floors
- Interactive elevator controls (call elevator up/down)
- Real-time status display for each elevator (current floor, moving/idle, target floor)
- Modular React components for easy extension
- Styled with Material-UI for a modern look

## Project Structure
```
my-app/
├── package.json
├── public/
│   ├── favicon.ico.png
│   ├── index.html
│   ├── manifest.json
│   └── robots.txt
├── src/
│   ├── App.js
│   ├── App.css
│   ├── index.js
│   ├── index.css
│   ├── logo.svg
│   ├── reportWebVitals.js
│   ├── setupTests.js
│   ├── components/
│   │   ├── AddBuilding.js
│   │   ├── AddBuilding.css
│   │   ├── Building.js
│   │   ├── Building.css
│   │   ├── BuildingView.js
│   │   ├── BuildingView.css
│   │   ├── Elevator.js
│   │   ├── Elevator.css
│   │   ├── ElevatorBuildingComponentt.js
│   │   ├── Login.js
│   │   ├── Login.css
│   │   └── servers/
│   │       ├── BuildingService.js
│   │       ├── ElevatorCallService.js
│   │       ├── ElevatorService.js
│   │       └── UserService.js
│   └── redux/
│       ├── ActionCreator.js
│       ├── Reducer.js
│       └── Store.js
└── README.md
```

## Main Components
- **Building.js**: Visualizes the building and elevator, handles floor requests.
- **Elevator.js**: Represents the elevator car and its state.
- **AddBuilding.js**: Allows adding new buildings to the system.
- **BuildingView.js**: Displays a list or details of buildings.
- **Login.js**: Handles user authentication (if implemented).

## State Management
Uses Redux for managing application state, including elevator status, user actions, and building data.

## Services
Located in `src/components/servers/`, these handle API calls and business logic for buildings, elevators, and users.

## Getting Started
1. Install dependencies:
   ```bash
   npm install
   ```
2. Start the development server:
   ```bash
   npm start
   ```
3. Open [http://localhost:3000](http://localhost:3000) in your browser.

## Technologies Used
- React
- Material-UI
- Redux

# Project Structure

This project consists of two main directories:

- **backend**: Contains the ASP.NET Core project for the server-side API.

- **frontend**: Contains the React project for the client-side application.

## Running the Project

To run the project, follow these steps:

### Backend (ASP.NET Core)

1. Open the backend directory in your terminal.
2. Ensure you have the .NET SDK installed.
3. Run the following command to restore dependencies:

```bash
   dotnet restore
```

4. Set up the SQL Server database using SQL Server Management Studio (SSMS).
5. Update the connection string in `appsettings.json` to point to your SQL Server instance.
6. Run the following command to start the server:

```bash
dotnet run
```

### Frontend (React)

1. Open the frontend directory in your terminal.
2. Ensure you have Node.js and npm installed.
3. Run the following command to install dependencies:

```bash
npm install
```

4. Start the React application with:

```bash
npm start
```

## Technology Choices

- **Backend:** ASP.NET Core was chosen for its robust API capabilities and ease of integration with SQL Server.
- **Frontend:** React.js was selected for its component-based architecture, allowing for a dynamic user interface.
- **Database:** SQL Server is used for its reliability and support for complex queries.
- **Real-time** Communication: SignalR is implemented for real-time updates between the server and client.

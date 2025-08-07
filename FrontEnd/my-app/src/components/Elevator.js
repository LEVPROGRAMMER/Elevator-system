import React, { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import './Elevator.css';
const Elevator = () => {
    const [elevatorStatus, setElevatorStatus] = useState(null);
    const [connection, setConnection] = useState(null);

    useEffect(() => {
        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7229/elevatorHub")
            .build();

        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(() => console.log("Connected to ElevatorHub"))
                .catch(err => console.error("Failed to connect: " + err.toString()));

            connection.on("ReceiveElevatorUpdate", (status) => {
                console.log("Received update:", status);
                setElevatorStatus(status);
            });
            
            return () => {
                connection.stop();
            };
        }
    }, [connection]); 

    const callElevator = (requestedFloor, destinationFloor) => {
        if (connection) {
            connection.invoke("CallElevator", requestedFloor, destinationFloor)
                .catch(err => console.error("Error invoking CallElevator: " + err.toString()));
        }
    };
    
    return (
        <div>
            <h1>Elevator Status</h1>
            {elevatorStatus && (
                <div>
                    <p>Current Floor: {elevatorStatus.currentFloor}</p>
                    <p>Status: {elevatorStatus.status}</p>
                    <p>Target Floors: {elevatorStatus.targetFloors && elevatorStatus.targetFloors.join(", ")}</p>
                </div>
            )}
            {elevatorStatus && (
                <div className="elevator">
                    <div>
                        Elevator
                        <div className="status">
                            <p>Status: {elevatorStatus.status}</p>
                            <p>Direction: {elevatorStatus.direction}</p>
                            <p>Current Floor: {elevatorStatus.currentFloor}</p>
                            <p>Door Status: {elevatorStatus.doorStatus}</p>
                        </div>
                    </div>
                    <div className="floor-buttons">
                        {Array.from({ length: 3 }, (_, index) => (
                            <button key={index + 1} onClick={() => callElevator(elevatorStatus.currentFloor, index + 1)}>
                                {index + 1}
                            </button>
                        ))}
                    </div>
                </div>
            )}
        </div>
    );
};

export default Elevator;
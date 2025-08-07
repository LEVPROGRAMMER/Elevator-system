import React, { useEffect, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import Elevator from '../components/Elevator'; // ודא שהנתיב נכון

const ElevatorServer = () => {
    const [elevatorStatus, setElevatorStatus] = useState(null);
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/elevatorHub")
        .build();

    useEffect(() => {
        connection.start()
            .then(() => console.log("Connected to ElevatorHub"))
            .catch(err => console.error(err.toString()));

        connection.on("ReceiveElevatorUpdate", (status) => {
            setElevatorStatus(status);
        });
    }, [connection]);

    const callElevator = (requestedFloor, destinationFloor) => {
        connection.invoke("CallElevator", requestedFloor, destinationFloor);
    };

    return (
        <div>
            <h1>Elevator Status</h1>
            {elevatorStatus && (
                <div>
                    <p>Current Floor: {elevatorStatus.currentFloor}</p>
                    <p>Status: {elevatorStatus.status}</p>
                    <p>Target Floors: {elevatorStatus.targetFloors.join(", ")}</p>
                </div>
            )}
            <button onClick={() => callElevator(3, 5)}>Call Elevator to Floor 3</button>
            {elevatorStatus && (
                <Elevator 
                    currentFloor={elevatorStatus.currentFloor} 
                    floorNumber={elevatorStatus.currentFloor}
                    status={elevatorStatus.status} 
                    direction={elevatorStatus.direction} 
                    doorStatus={elevatorStatus.doorStatus} 
                    showFloorSelection={true} 
                    handleFloorSelection={(floor) => callElevator(elevatorStatus.currentFloor, floor)} 
                />
            )}
        </div>
    );
};

export default ElevatorServer;

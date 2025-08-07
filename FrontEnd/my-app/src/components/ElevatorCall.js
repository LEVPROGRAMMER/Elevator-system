import './ElevatorCall.css';
import React, { useEffect, useState } from 'react';
import Elevator from './Elevator';
import { addElevatorCall } from '../servers/ElevatorCallServer';
const ElevatorCall = (props) => {
    const [selectedFloor, setSelectedFloor] = useState(null);
    const [showFloorSelection, setShowFloorSelection] = useState(false);
    const elevatorCallId = 1;

    const handleFloorSelection = async (floor) => {
        console.log("gfgs")
        setSelectedFloor(floor);
        setShowFloorSelection(false);
    };

    const handleButtonClick = () => {
        const data = {
            id: 5642,
            buildingId: props.building, 
            requestedFloor: props.floor,
            destinationFloor: selectedFloor,
            callTime: new Date().toISOString(), 
            isHandled: true,
        };
        addElevatorCall(data)
            .then(data => console.log('Elevator call added:', data))
            .catch(error => console.error('Error:', error));

        setShowFloorSelection(true); 
    };

    useEffect(() => {
        handleButtonClick();
    }, []); 
    return (
        <div>
        </div>
    );
};

export default ElevatorCall;

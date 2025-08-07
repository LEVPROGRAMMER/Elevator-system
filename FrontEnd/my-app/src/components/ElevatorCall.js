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
            buildingId: props.building, // תזין את ה-ID של הבניין כאן
            requestedFloor: props.floor,
            destinationFloor: selectedFloor,
            callTime: new Date().toISOString(), // שעה נוכחית בפורמט ISO
            isHandled: true,
        };
        addElevatorCall(data)
            .then(data => console.log('Elevator call added:', data))
            .catch(error => console.error('Error:', error));

        setShowFloorSelection(true); // להציג את בחירת הקומות לאחר הצלחה
    };

    useEffect(() => {
        handleButtonClick();
    }, []); // ריק כדי להפעיל רק בטעינה הראשונה

    return (
        <div>
            {/* <div>{elevatorCallId}</div> */}
        </div>
    );
};

export default ElevatorCall;

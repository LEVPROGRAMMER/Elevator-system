import React, { useEffect, useState } from 'react';
import './Building.css';
import ElevatorCall from './ElevatorCall';
import Elevator from './Elevator';
import { updateElevatorCall } from '../servers/ElevatorCallServer';
import { fetchBuildingData } from '../servers/BuildingServer';
import AddBuilding from './AddBuilding';
import BuildingView from './BuildingView';

const Building = (promps) => {
    debugger
const [data, setData] = useState({});
const [showElevator, setShowElevator] = useState(false);
const [currentFloor, setCurrentFloor] = useState(0);
const [status, setStatus] = useState('Idle');
const [direction, setDirection] = useState('none');
const [doorStatus, setDoorStatus] = useState('closed');
const [activeFloor, setActiveFloor] = useState(null);
const [showFloorSelection, setShowFloorSelection] = useState(false); // מצב חדש

const handleButtonClick = (floor, direction) => {
    setActiveFloor(floor);
    setShowElevator(true);
    setCurrentFloor(floor);
    setDirection(direction);
    setShowFloorSelection(true); 

   
};

const ElevatorButton = ({ floor, direction }) => {
    const isBlinking = activeFloor === floor;
    return (
        <button
            className={isBlinking ? 'blink' : ''}
            onClick={() => handleButtonClick(floor, direction)}
        >
            {`Call Elevator ${direction === 'up' ? '▲' : '▼'}`}
        </button>
    );
};

return (
    <div>
    <div className="building">
        <h1>Building: {promps.building.name}</h1>
        <div className="floors">
            {Array.from({ length: promps.building.numberOfFloors }, (_, index) => {
                const floorNumber = promps.building.numberOfFloors - 1 - index;
                return (
                    <div key={index} className={`floor ${currentFloor === floorNumber ? 'active' : ''}`}>
                        {currentFloor === floorNumber && (
                            <Elevator
                                currentFloor={currentFloor}
                                floorNumber={floorNumber}
                                status={status}
                                direction={direction}
                                doorStatus={doorStatus}
                                showFloorSelection={showFloorSelection} 
                                handleFloorSelection={(floor) => {
                                    updateElevatorCall(2 ,"DestinationFloor",floor)
                                    setCurrentFloor(floor);
                                    setShowFloorSelection(false); 
                                }}
                            />
                        )}
                        <div className="floor-number">{`Floor ${floorNumber}`}</div>
                        <ElevatorButton
                            floor={floorNumber}
                            direction="up"
                        />
                        <ElevatorButton
                            floor={floorNumber}
                            direction="down"
                        />
                    </div>
                );
            })}
        </div>
        {showElevator && <ElevatorCall floor={currentFloor} building={promps.building.id} direction={direction} />}
       
    </div>
    </div>
);
};

export default Building;


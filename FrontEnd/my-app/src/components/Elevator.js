import React from 'react';

const Elevator = ({ currentFloor, floorNumber, status, direction, doorStatus, showFloorSelection, handleFloorSelection }) => {
    return (
        <div className="elevator">
            {currentFloor === floorNumber && (
                <>
                    <div>
                        elevator
                        <div className="status">
                            <p>status: {status}</p>
                            <p>direction: {direction}</p>
                            <p>current floor: {currentFloor}</p>
                            <p>door status: {doorStatus}</p>
                        </div>
                    </div>
                </>
            )}
            {showFloorSelection && (
                <div className="floor-buttons">
                    {Array.from({ length: 3 }, (_, index) => (
                        <button key={index + 1} onClick={() => handleFloorSelection(index + 1)}>
                            {index + 1}
                        </button>

                    ))}
                </div>
            )}
        </div>
    );
};

export default Elevator;

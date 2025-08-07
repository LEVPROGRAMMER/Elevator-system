import React, { useState, useEffect } from 'react';
import './BuildingView.css';
import AddBuilding from './AddBuilding';
import { useSelector } from 'react-redux';
import Building from './Building';

function BuildingView() {
    const [buildings, setBuildings] = useState([]);
    const [elevators, setElevators] = useState([]);
    const [elevatorCalls, setElevatorCalls] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [callFloor, setCallFloor] = useState('');
    const userId = useSelector(state => state.userId); 
    const [selectedBuilding, setSelectedBuilding] = useState(null);

    useEffect(() => {
        fetchBuildingData();
    }, [userId]);

    const fetchBuildingData = async () => {
        try {
            setLoading(true);

            const buildingResponse = await fetch(`https://localhost:7229/api/Building/GetBuildingByUser/Id?Id=${userId}`);
            if (buildingResponse.ok) {
                const buildingData = await buildingResponse.json();
                setBuildings(buildingData);
            }

            const elevatorsResponse = await fetch('https://localhost:7229/api/Elevator/GetAllElevator');
            if (elevatorsResponse.ok) {
                const allElevators = await elevatorsResponse.json();
                const buildingElevators = allElevators.filter(elevator =>
                    elevator.buildingId === buildings.buildingId || allElevators.length === 1
                );
                setElevators(buildingElevators);
            }

            try {
                const callsResponse = await fetch('https://localhost:7229/api/ElevatorCalls/GetAllElevatorCalls');
                if (callsResponse.ok) {
                    const allCalls = await callsResponse.json();
                    setElevatorCalls(allCalls);
                }
            } catch (callError) {
                console.log('No elevator calls available');
            }

        } catch (err) {
            setError('Error loading building data');
            console.error('Error fetching building data:', err);
        } finally {
            setLoading(false);
        }
    };

    const openBuilding = (building) => {
        setSelectedBuilding(building); 
    };


    const handleElevatorCall = async () => {
        if (!callFloor || callFloor < 1) {
            setError('Please enter a valid floor');
            return;
        }

        try {
            const callData = {
                fromFloor: parseInt(callFloor),
                toFloor: parseInt(callFloor) + 1,
                requestTime: new Date().toISOString(),
                status: 'Pending'
            };

            const response = await fetch('https://localhost:7229/api/ElevatorCalls/AddElevatorCall', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(callData),
            });

            if (response.ok) {
                setCallFloor('');
                fetchBuildingData();
                setError('');
            } else {
                setError('Error calling elevator');
            }
        } catch (err) {
            setError('Error calling elevator');
            console.error('Error calling elevator:', err);
        }
    };

    if (loading) {
        return (
            <div className="building-view-container">
                <div className="loading">Loading building data...</div>
            </div>
        );
    }

    return (
        <div>
            <div className="building-view-container">
                <div className="building-view-wrapper">
                    <div className="header-section">
                        <h1>Building:</h1>
                    </div>

                    {error && (
                        <div className="error-message">{error}</div>
                    )}

                    <div className="building-info">
                        <div className="info-card">
                            {buildings.map((building, index) => (
                                <div key={index} className="info-card" onClick={() => openBuilding(building)}>
                                    <h3>Building Details</h3>
                                    <p><strong>Name:</strong> {building?.name}</p>
                                    <p><strong>Number of Floors:</strong> {building?.numberOfFloors || 'Not available'}</p>
                                </div>
                            ))}
                        </div>

                        <AddBuilding></AddBuilding>
                   
                    </div>

                  
                </div>

            </div>
            <div className="building">
            {selectedBuilding && <Building building={selectedBuilding} />}
</div>
        </div>
    );
};
export default BuildingView;

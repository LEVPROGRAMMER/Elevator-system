import React, { useState, useEffect } from 'react';
import './BuildingView.css';
import AddBuilding from './AddBuilding';
import { useSelector } from 'react-redux';
import ElevatorBuildingComponentt from './ElevatorBuildingComponentt';
import { getBuilding } from '../components/servers/BuildingService';
import { getElevatorByBuilding } from '../components/servers/ElevatorService';
import { getElevatorCalls } from '../components/servers/ElevatorCallService';

function BuildingView() {
    const [buildings, setBuildings] = useState([]);
    const [elevators, setElevators] = useState([]);
    const [elevatorCalls, setElevatorCalls] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const userId = useSelector(state => state.userId);
    const [selectedBuilding, setSelectedBuilding] = useState(null);

    useEffect(() => {
        const fetchBuildingData = async () => {
            try {
                setLoading(true);                                
                const buildingData = await getBuilding(userId);
                setBuildings(buildingData)
            } catch (err) {
                setError('Error loading building data');
                console.error('Error fetching building data:', err);
            } finally {
                setLoading(false);
            }
        };
        fetchBuildingData();
    }, [userId]);

    const openBuilding = (building) => {
        setSelectedBuilding(building);
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
            {!selectedBuilding &&
            <div className="building-view-container">
                <div className="building-view-wrapper">
                    <div className="header-section">
                        <h1>Building</h1>
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
            }
            <div className="building">
                {selectedBuilding && <ElevatorBuildingComponentt props={selectedBuilding} />}
            </div>
        </div>
    );
}
export default BuildingView;

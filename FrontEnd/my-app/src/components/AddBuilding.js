import React, { useState } from 'react';
import './AddBuilding.css';
import { useSelector } from 'react-redux';

function AddBuilding() {
    const [buildingData, setBuildingData] = useState({
        userId: '',
        name: '',
        numberOfFloors: ''
    });

    const [message, setMessage] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const userId = useSelector(state => state.userId); 

    const handleChange = (e) => {
        const { name, value } = e.target;
        setBuildingData({
            ...buildingData,
            [name]: value,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setMessage('');
        setIsLoading(true);
        debugger
        try {
            debugger
            const building = {
                id: 1,
                userId: userId,
                name: buildingData.name,
                numberOfFloors: parseInt(buildingData.floors)
            };
            const buildingResponse = await fetch('https://localhost:7229/api/Building/AddBuilding/Building', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(building),
            });

            if (!buildingResponse.ok) {
                throw new Error('Error adding building');
            }

            const buildingResult = await buildingResponse.json();
            console.log('Building created:', buildingResult);

            const elevator = {
                buildingId: buildingResult.id || 1,
                currentFloor: 0,
                status: 0,
                direction: 0,
                doorStatus: 0,
                targetFloors: []
            };

            const elevatorResponse = await fetch('https://localhost:7229/api/Elevator/AddElevator', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(elevator),
            });

            if (!elevatorResponse.ok) {
                throw new Error('Error creating elevator');
            }

            const elevatorResult = await elevatorResponse.json();
            console.log('Elevator created:', elevatorResult);

            setMessage(`Building "${buildingData.name}" created successfully with automatic elevator!`);

            setBuildingData({
                userId: "",
                name: '',
                numberOfFloors: ''
            });

        } catch (error) {
            console.error('Error:', error);
            setMessage(error.message || 'Error creating building');
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="add-building-container">
            <div className="form-wrapper">
                <div className="header-with-back">
                    <h2>Add New Building</h2>
                </div>

                <form onSubmit={handleSubmit}>
                    <div className="input-group">
                        <label htmlFor="name">Building Name:</label>
                        <input
                            type="text"
                            id="name"
                            name="name"
                            value={buildingData.name}
                            onChange={handleChange}
                            placeholder="Enter building name"
                            required
                            minLength="2"
                        />
                    </div>

                    <div className="input-group">
                        <label htmlFor="floors">Number of Floors:</label>
                        <input
                            type="number"
                            id="floors"
                            name="floors"
                            value={buildingData.floors}
                            onChange={handleChange}
                            placeholder="Enter number of floors"
                            required
                            min="1"
                            max="100"
                        />
                    </div>

                    <button
                        type="submit"
                        disabled={isLoading}
                        className={isLoading ? 'loading' : ''}
                    >
                        {isLoading ? 'Creating Building...' : 'Create Building + Elevator'}
                    </button>
                </form>

                {message && (
                    <div className={`message ${message.includes('successfully') ? 'success' : 'error'}`}>
                        {message}
                    </div>
                )}

            </div>
        </div>
    );
}

export default AddBuilding;

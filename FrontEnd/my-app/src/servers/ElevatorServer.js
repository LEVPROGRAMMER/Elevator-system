import React, { useEffect } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';

const ElevatorComponent = () => {
    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl('https://localhost:7229/ElevatorHub')
            .withAutomaticReconnect()
            .build();

        connection.start()
            .then(() => console.log('Connected to SignalR hub'))
            .catch(err => console.error('Error connecting to SignalR hub:', err));

        // כאן תוכל להוסיף מאזינים לאירועים מה-SignalR
        connection.on('ElevatorCallHandled', (data) => {
            console.log('Received data:', data);
        });

        // נקה את החיבור כשמרכיב ה-React מתפרק
        return () => {
            connection.stop();
        };
    }, []);

    return (
        <div>
            <h1>Elevator Control</h1>
            {/* הוסף כאן את רכיבי המעלית שלך */}
        </div>
    );
};

export default ElevatorComponent;

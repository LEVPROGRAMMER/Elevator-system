import React, { useState, useEffect } from 'react';
import Login from './Login';

function App() {
  
    const [data, setData] = useState([]);

    useEffect(() => {
        fetch('https://localhost:7229/api/Building/GetAllBuilding')
            .then(response => response.json())
            .then(data => setData(data))
            .catch(error => console.error('Error fetching data:', error));
    }, []); // [] מבטיח שהקריאה תתבצע רק פעם אחת כשהקומפוננטה נטענת

    return (
        <div className="App">
            <header>
              <Login></Login>
                {/* <div>{JSON.stringify(data)}</div> */}
            </header>
        </div>
    );
}
// function Add() {
//     const [elevatorData, setElevatorData] = useState({
//         // כאן תוסיף את השדות הרלוונטיים שאתה רוצה לשלוח
//         name: '',
//         capacity: 0,
//     });

//     const handleChange = (e) => {
//         const { name, value } = e.target;
//         setElevatorData({
//             ...elevatorData,
//             [name]: value,
//         });
//     };

//     const handleSubmit = (e) => {
//         e.preventDefault();

//         fetch('https://localhost:7229/api/Elevator/AddElevator', {
//             method: 'POST',
//             headers: {
//                 'Content-Type': 'application/json',
//             },
//             body: JSON.stringify(elevatorData),
//         })
//         .then(response => response.json())
//         .then(data => {
//             console.log('Success:', data);
//         })
//         .catch((error) => {
//             console.error('Error:', error);
//         });
//     };

//     return (
//         <div className="App">
//             <form onSubmit={handleSubmit}>
//                 <input
//                     type="text"
//                     name="name"
//                     value={elevatorData.name}
//                     onChange={handleChange}
//                     placeholder="Elevator Name"
//                     required
//                 />
//                 <input
//                     type="number"
//                     name="capacity"
//                     value={elevatorData.capacity}
//                     onChange={handleChange}
//                     placeholder="Elevator Capacity"
//                     required
//                 />
//                 <input
//                     type="number"
//                     name="capacity"
//                     value={elevatorData.capacity}
//                     onChange={handleChange}
//                     placeholder="Elevator Capacity"
//                     required
//                 />
//                 <button type="submit">Add Elevator</button>
//             </form>
//         </div>
//     );
// }
export default  App;

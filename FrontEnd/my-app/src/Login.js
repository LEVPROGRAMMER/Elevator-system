import React, { useState } from 'react';
import '../src/Login.css'
function Login() {
    const [userData, setUserData] = useState({
        id:'',
        email: '',
        password: '',
    });

    const handleChange = (e) => {
        const { id, value } = e.target;
        setUserData({
            ...userData,
            [id]: value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        // בדוק אם המשתמש קיים
        fetch(`https://localhost:7229/api/User/GetUserById/Id?Id=${userData.id}`)
            .then(response => response.json())
            .then(exists => {
                if (!exists) {
                    // אם המשתמש לא קיים, בצע רישום
                    fetch('https://localhost:7229/api/User/AddUser/User', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(userData),
                    })
                    .then(response => response.json())
                    .then(data => {
                        console.log('Registration Success:', data);
                    })
                    .catch((error) => {
                        console.error('Registration Error:', error);
                    });
                } else {
                    console.log('User already exists, no action taken.');
                }
            })
            .catch((error) => {
                console.error('Check Exists Error:', error);
            });
    };

    return (
        <div className="App">
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    name="email"
                    value={userData.email}
                    onChange={handleChange}
                    placeholder="email"
                    required
                />
                <input
                    type="password"
                    name="password"
                    value={userData.password}
                    onChange={handleChange}
                    placeholder="Password"
                    required
                />
                <button type="submit">Submit</button>
            </form>
        </div>
    );
}

export default Login;

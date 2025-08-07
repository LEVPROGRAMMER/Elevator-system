import React, { useState } from 'react';
import './Login.css';
import Building from './Building';
import { fetchUserData } from '../servers/UserServer';
import BuildingView from './BuildingView';
import { useDispatch, useSelector } from 'react-redux';
import { setUserId } from '../redux/ActionCreator';

function Login() {
    const [userData, setUserData] = useState({
        id: '',
        email: '',
        password: '',
    });
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [isRegistering, setIsRegistering] = useState(false); 
       const dispatch = useDispatch(); 

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUserData({
            ...userData,
            [name]: value,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const data = await fetchUserData(userData);
        debugger;

        setUserData({ ...userData, id: data.id });
            dispatch(setUserId(data.id)); 

        setIsLoggedIn(true);
    };

    const toggleRegistering = () => {
        setIsRegistering(!isRegistering); 
    };

    if (isLoggedIn) {
        return <BuildingView />;
    }

    return (
        <div className="App">
            <form onSubmit={handleSubmit}>
                {isRegistering && (
                    <input
                        type="text"
                        name="id"
                        value={userData.id}
                        onChange={handleChange}
                        placeholder="ID"
                        required={isRegistering}
                    />
                )}
                <input
                    type="text"
                    name="email"
                    value={userData.email}
                    onChange={handleChange}
                    placeholder="Email"
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
                <button type="submit">{isRegistering ? 'Register' : 'Login'}</button>
            </form>
            <button onClick={toggleRegistering}>
                {isRegistering ? 'Switch to Login' : 'Switch to Register'}
            </button>
        </div>
    );
}

export default Login;

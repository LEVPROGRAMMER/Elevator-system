import React, { useState } from 'react';
import './Login.css';
import { getUser, createUser } from '../components/servers/UserService';
import BuildingView from './BuildingView';
import { useDispatch } from 'react-redux';
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
        let data;
        try {
            data = (await getUser(userData.password));
            if (data.length == 0) {
                data = await createUser(userData);
            }
            setUserData({ ...userData, id: data.id });
            
            dispatch(setUserId(userData.id || data.id));
            setIsLoggedIn(true);
        } catch (err) {
            alert('Error during login/registration');
        }
    };

    const toggleRegistering = () => {
        console.log('Toggling registration state');
        setIsRegistering(!isRegistering);
    };

    if (isLoggedIn) {
        return <BuildingView />;
    }

    return (
        <div className="auth-container">
            <div className="login-card">
                <h2 className="login-title">{isRegistering ? 'Register' : 'Login'}</h2>
                <form className="auth-form" onSubmit={handleSubmit}>
                    {isRegistering && (
                        <div className="form-group">
                            <label htmlFor="id">ID</label>
                            <input
                                type="text"
                                name="id"
                                value={userData.id}
                                onChange={handleChange}
                                placeholder="ID"
                                required={isRegistering}
                            />
                        </div>
                    )}
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input
                            type="text"
                            name="email"
                            value={userData.email}
                            onChange={handleChange}
                            placeholder="Email"
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            name="password"
                            value={userData.password}
                            onChange={handleChange}
                            placeholder="Password"
                            required
                        />
                    </div>
                    <button className="submit-button" type="submit">{isRegistering ? 'Register' : 'Login'}</button>
                </form>
                <button className="toggle-button" onClick={toggleRegistering}>
                    {isRegistering ? 'Switch to Login' : 'Switch to Register'}
                </button>
            </div>
        </div>
    );
}

export default Login;

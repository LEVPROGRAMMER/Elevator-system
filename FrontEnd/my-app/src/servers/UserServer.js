import axios from 'axios';

export const fetchUserData = async (userData) => {
    try {
        const response = await axios.get(`https://localhost:7229/api/User/GetUserByPassword/password`, {
            params: { password: userData.password }
        });
        if (!response.data) {
            const registrationResponse = await axios.post('https://localhost:7229/api/User/AddUser/User', userData);
            console.log('Registration Success:', registrationResponse.data);
            return registrationResponse.data; 
        } else {
            return response.data; 
        }

    } catch (error) {
        console.error('Error fetching user data:', error);
    }
};

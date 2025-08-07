// const handleSubmit = (e) => {
//     e.preventDefault();

//     axios.get(`https://localhost:7229/api/User/GetUserByPassword/password`, {
//         params: { password: userData.password }
//     })
//         .then(response => {
//             const exists = response.data; // הנחה שה-ID נמצא ב-response.data.exists
//             if (!exists) {
//                 axios.post('https://localhost:7229/api/User/AddUser/User', userData)
//                     .then(response => {
//                         console.log('Registration Success:', response.data);
//                     })
//                     .catch((error) => {
//                         console.error('Registration Error:', error);
//                     });
//             } else {
//                 // עדכון הסטייט עם הנתונים שהתקבלו מהשרת
               
//         })
//         .catch((error) => {
//             console.error('Check Exists Error:', error);
//         });
// };

import axios from 'axios';

export const fetchUserData = async (userData) => {
    try {
        const response = await axios.get(`https://localhost:7229/api/User/GetUserByPassword/password`, {
            params: { password: userData.password }
        });

        console.log(response.data);
        if (!response.data) {
            // אם אין נתונים, נבצע הרשמה
            const registrationResponse = await axios.post('https://localhost:7229/api/User/AddUser/User', userData);
            console.log('Registration Success:', registrationResponse.data);
            return registrationResponse.data; // החזרת נתוני ההרשמה אם נדרש
        } else {
            return response.data; // החזרת נתוני המשתמש אם נמצאו
        }

    } catch (error) {
        console.error('Error fetching user data:', error);
    }
};

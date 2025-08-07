import axios from 'axios';
import ElevatorServer from './ElevatorServer';
const url = 'https://localhost:7229/api/ElevatorCalls/'

export const addElevatorCall = async (elevatorCall) => {
      debugger
    try {
        const response = await axios.post(`${url}AddElevatorCall/ElevatorCall`, elevatorCall);
        return response.data;
    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}


export const updateElevatorCall = async (id, fileName, value) => {
    debugger
    <ElevatorServer></ElevatorServer>
    try {
        const response = await axios.put(`${url}UpDateElevatorCall/${id}`,fileName, value );
        console.log(response.data);
    } catch (error) {
        console.error("Error updating elevator call:", error);
    }
};

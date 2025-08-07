import axios from 'axios';

export const fetchBuildingData = async (id) => {
    try {
        const response = await axios.get(`https://localhost:7229/api/Building/GetBuildingByUser/Id?Id=${id}`);
                console.log(response.data);

        return response.data;

    } catch (error) {
        console.error('Error fetching building data:', error);
    }
};

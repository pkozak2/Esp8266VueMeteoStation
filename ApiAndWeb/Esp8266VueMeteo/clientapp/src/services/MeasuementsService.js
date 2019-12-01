import axios from '@/plugins/axios';
let apiUrl = 'api/data'
export default{
    GetUserDevices(username){
        return axios.get(`${apiUrl}/sensors/${username}`);
    },
    GetCurrentMeasurements(deviceId){
        return axios.get(`${apiUrl}/measurements/${deviceId}`);
    },
    GetMeasurementsFromLastHours(deviceId, hours){
        return axios.get(`${apiUrl}/measurements/${deviceId}/${hours}`);
    }
}

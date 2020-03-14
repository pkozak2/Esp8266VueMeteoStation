import axios from "@/plugins/axios";
let apiUrl = "/api/devices";
export default {
  GetCurrentMeasurements(deviceId) {
    return axios.get(`${apiUrl}/${deviceId}/data`);
  },
  GetAverages(deviceId) {
    return axios.get(`${apiUrl}/${deviceId}/averages`);
  }
};

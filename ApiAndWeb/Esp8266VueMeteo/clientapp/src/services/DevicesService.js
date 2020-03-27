import axios from "@/plugins/axios";
let apiUrl = "/api/devices";
export default {
  GetCurrentMeasurements(deviceId) {
    return axios.get(`${apiUrl}/${deviceId}/data`);
  },
  GetAverages(deviceId, hours) {
    return axios.get(`${apiUrl}/${deviceId}/averages/${hours}`);
  },
  GetPollutionGraphData(deviceId, days) {
    return axios.get(`${apiUrl}/${deviceId}/averages/pollution/${days}`);
  },
  GetTemperatureGraphData(deviceId, days) {
    return axios.get(`${apiUrl}/${deviceId}/averages/temperature/${days}`);
  }
};

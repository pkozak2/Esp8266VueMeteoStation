import axios from "@/plugins/axios";
let apiUrl = "/api/users";
export default {
  GetUserDevices(userName) {
    return axios.get(`${apiUrl}/${userName}/devices`);
  },
  GetUserDevice(userName, deviceNormalizedName) {
    return axios.get(`${apiUrl}/${userName}/devices/${deviceNormalizedName}`);
  }
};

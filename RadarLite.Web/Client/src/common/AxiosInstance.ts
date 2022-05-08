import axios from "axios";

const AxiosInstance = axios.create({
  baseURL: "https://localhost:44317/",
});

//axios.defaults.headers.common["Authorization"] = "Bearer " + r.data.token;

AxiosInstance.interceptors.request.use((config) => {
  //NEVER use local storage for user info or anything sensitive
  //call store
  ///const store = new authStore();
  //use store to auth
  //store.
  //const test = JSON.parse(localStorage.getItem("user")).token;
});

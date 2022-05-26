import axios, { AxiosResponse } from "axios";
import {
  DbConcurrencyError,
  ErrorResponse,
  InternalServerError,
  UnauthorizedError,
} from "./ErrorModels";

const AxiosInstance = axios.create({
  baseURL: "https://localhost:5000",
});

//axios.defaults.headers.common["Authorization"] = "Bearer " + r.data.token;

// AxiosInstance.interceptors.request.use((config) => {
//   //NEVER use local storage for user info or anything sensitive
//   //call store
//   ///const store = new authStore();
//   //use store to auth
//   //store.
//   //const test = JSON.parse(localStorage.getItem("user")).token;
// });

AxiosInstance.interceptors.response.use(
  (response: AxiosResponse<any>) => response,
  ({ response }: { response: AxiosResponse<string> }) => {
    switch (response.status) {
      case 409:
        return Promise.reject(
          new DbConcurrencyError(response.data, response.status)
        );
      case 500:
        return Promise.reject(
          new InternalServerError(response.data, response.status)
        );
      case 404:
        return Promise.reject(
          new UnauthorizedError(response.data, response.status)
        );
    }
    // Generic Error Response
    return new ErrorResponse(response.data, response.status);
  }
);

export default AxiosInstance;

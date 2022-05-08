import UserModel from "@/common/UserModel";
import { deserialize } from "@/Helpers/JsonMapper";
import axios, { AxiosResponse } from "axios";
import { authStore } from "@/stores/AuthStore";

const API_URL = "https://localhost:7056/api/auth/";

/**
 * AuthenticationService
 * Service for identity purposes
 *
 * @class
 */
class AuthenticationService {
  async Login(user: UserModel) {
    axios.defaults.timeout = 25000;
    const path = "https://localhost:44317/nws/healthy";
    let response: AxiosResponse<JsonMapper.IGenericObject>;
    const store = new authStore();

    try {
      if (store.isAuth != true) {
        console.log("User Not Logged In");
        //redirect them away? idk..
      }
      response = await axios.get<JsonMapper.IGenericObject>(path);

      return deserialize(UserModel, response.data);
    } catch (e) {
      console.log("Error retrieiving connection with identity server. Timeout");
      return new UserModel();
    }
  }
}

// axios.defaults.timeout = 25000;
//   const path = "https://localhost:44317/nws/healthy"; //"http://192.168.1.192:7506/Cities";
//   let response: AxiosResponse<JsonMapper.IGenericObject>;

//   try {
//     response = await axios.get<JsonMapper.IGenericObject>(path);
//     return deserialize(HealthModel, response.data);
//   } catch (e) {
//     console.log("Error retrieiving connection with server. Timeout");
//     return new HealthModel();
//   }

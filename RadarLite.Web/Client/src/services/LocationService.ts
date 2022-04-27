import axios, { AxiosResponse } from "axios";
import HealthModel from "@/common/HealthModel";
import { deserialize, DeserializeArray } from "@/helpers/JsonMapper";

export async function GetHealthAsync(): Promise<HealthModel> {
  axios.defaults.timeout = 5000;
  const path = "https://localhost:7264/nws/healthy"; //"http://192.168.1.192:7506/Cities";
  let response: AxiosResponse<JsonMapper.IGenericObject>;

  try {
    response = await axios.get<JsonMapper.IGenericObject>(path);
    return deserialize(HealthModel, response.data);
  } catch (e) {
    console.log("Error retrieiving connection with server. Timeout");
    return new HealthModel();
  }
}

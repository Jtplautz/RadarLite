import axios, { AxiosResponse } from "axios";
import HealthModel from "@/common/HealthModel";
import { deserialize, DeserializeArray } from "@/helpers/JsonMapper";

export async function GetHealthAsync(): Promise<HealthModel> {
  const path = "https://localhost:7264/nws/healthy"; //"http://192.168.1.192:7506/Cities";
  const response: AxiosResponse<JsonMapper.IGenericObject> =
    await axios.get<JsonMapper.IGenericObject>(path);
  return deserialize(HealthModel, response.data);
}

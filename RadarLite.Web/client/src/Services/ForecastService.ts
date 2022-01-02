import axios, { AxiosResponse } from "axios";
import City from "@/common/City";
import { DeserializeArray } from "@/Helpers/JsonMapper";

async function GetCities(): Promise<City[]> {
  const path = "http://192.168.1.192:7500/api/city/NWS/cities";
  const response: AxiosResponse<Array<JsonMapper.IGenericObject>> =
    await axios.get<City[]>(path);
  return DeserializeArray(City, Object.values(response.data));
}

import axios, { AxiosResponse } from "axios";
import City from "@/common/City";
import Location from "@/common/Location";
import { DeserializeArray } from "@/Helpers/JsonMapper";

export async function GetCitiesAsync(): Promise<Location[]> {
  const path = "http://192.168.1.192:7506/Cities";
  const response: AxiosResponse<Array<JsonMapper.IGenericObject>> =
    await axios.get<Location[]>(path);
  return DeserializeArray(Location, Object.values(response.data));
}


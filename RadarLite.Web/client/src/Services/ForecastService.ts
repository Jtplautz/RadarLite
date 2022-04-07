import axios, { AxiosResponse } from "axios";
import City from "@/common/City";
import Location from "@/common/Location";
import { deserialize, DeserializeArray } from "@/Helpers/JsonMapper";

export async function GetCitiesAsync(): Promise<Location[]> {
  const path =
    "http://RadarLite.NationalWeatherService.me:7506/location/Cities"; //"http://localhost:7506/location/cities";//"http://192.168.254.125:7506/Cities"; //"http://192.168.254.125:7506/Cities";//"http://192.168.1.192:7506/Cities";
  const response: AxiosResponse<Array<JsonMapper.IGenericObject>> =
    await axios.get<JsonMapper.IGenericObject[]>(path);
  return DeserializeArray(Location, Object.values(response.data));
}
export async function GetCityAsync(zipcode: number): Promise<Location> {
  const path =
    "http://RadarLite.NationalWeatherService.me:7506/location/city/" + zipcode; //"http://192.168.1.192:7506/Cities";
  const response: AxiosResponse<JsonMapper.IGenericObject> =
    await axios.get<JsonMapper.IGenericObject>(path);
  return deserialize(Location, response.data);
}

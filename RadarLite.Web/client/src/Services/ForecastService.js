import axios from "axios";
import Location from "@/common/Location";
import { deserialize, DeserializeArray } from "@/Helpers/JsonMapper";
export async function GetCitiesAsync() {
    const path = "http://RadarLite.NationalWeatherService.me:7506/location/Cities"; //"http://localhost:7506/location/cities";//"http://192.168.254.125:7506/Cities"; //"http://192.168.254.125:7506/Cities";//"http://192.168.1.192:7506/Cities";
    const response = await axios.get(path);
    return DeserializeArray(Location, Object.values(response.data));
}
export async function GetCityAsync(zipcode) {
    const path = "http://RadarLite.NationalWeatherService.me:7506/location/city/" + zipcode; //"http://192.168.1.192:7506/Cities";
    const response = await axios.get(path);
    return deserialize(Location, response.data);
}
//# sourceMappingURL=ForecastService.js.map
import { ILocation } from "@/common/Types";
import { JsonProperty } from "@/Helpers/JsonMapper";

class Location implements ILocation, JsonMapper.IGenericObject {
  @JsonProperty("id")
  public id = "";

  @JsonProperty("name")
  public name = "";

  //   @JsonProperty(`county`)
  //   public county = "";

  //   @JsonProperty(`state`)
  //   public state = "";

  //   @JsonProperty(`zip`)
  //   public zip = 0;

  //   @JsonProperty(`Locationkey`)
  //   public locationKey = "";

  //   @JsonProperty(`timezoneCode`)
  //   public timezoneCode = "";

  // constructor() {
  //   console.log("a city was generated" + this.city);
  // }
}
export default Location;

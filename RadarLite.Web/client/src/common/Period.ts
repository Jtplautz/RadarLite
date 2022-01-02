import { Period } from "@/common/Types";
import { JsonProperty } from "@/Helpers/JsonMapper";

class ForecastPeriod implements Period, JsonMapper.IGenericObject {
  @JsonProperty(`detailedForecast`)
  public detailedForecast = "";

  @JsonProperty(`shortForecast`)
  public shortForecast = "";

  @JsonProperty(`icon`)
  public icon = "";

  @JsonProperty(`windDirection`)
  public windDirection = "";

  @JsonProperty(`windSpeed`)
  public windSpeed = "";

  @JsonProperty(`temperatureTrend`)
  public temperatureTrend = "";

  @JsonProperty(`temperatureUnit`)
  public temperatureUnit = "";

  @JsonProperty(`temperature`)
  public temperature = 0;

  @JsonProperty(`isDateTime`)
  public isDateTime = false;

  @JsonProperty(`endTime`)
  public endTime = Date;

  @JsonProperty(`startTime`)
  public startTime = Date;

  @JsonProperty(`name`)
  public name = "";

  @JsonProperty(`number`)
  public number = 0;
}

export default ForecastPeriod;

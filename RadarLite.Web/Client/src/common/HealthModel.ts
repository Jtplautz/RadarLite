import type { IHealthModel } from "@/common/Types";
import { JsonProperty } from "@/helpers/JsonMapper";

class HealthModel implements IHealthModel, JsonMapper.IGenericObject {
  @JsonProperty("id")
  public status = "";
}
export default HealthModel;

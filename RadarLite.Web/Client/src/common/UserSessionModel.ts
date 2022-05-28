import { JsonProperty } from "@/Helpers/JsonMapper";
class UserSessionModel implements JsonMapper.IGenericObject {
  @JsonProperty("type")
  public type: string | null = "";
  @JsonProperty("value")
  public value: string | null = "";
}
export default UserSessionModel;

import { JsonProperty } from "@/Helpers/JsonMapper";

class UserModel implements JsonMapper.IGenericObject {
  @JsonProperty("UserName")
  public userName = "";
}
export default UserModel;

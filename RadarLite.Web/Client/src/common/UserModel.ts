import { JsonProperty } from "@/Helpers/JsonMapper";

class UserModel implements JsonMapper.IGenericObject {
  @JsonProperty("UserName")
  public username: string | null = "";
  @JsonProperty("Password")
  public password: string | null = "";
  @JsonProperty("Email")
  public email: string | null = "";
}
export default UserModel;

// class UserSessionModelArray implements JsonMapper.IGenericObject {
//   public UserSessionModels: Array<UserSessionModel> =
//     new Array<UserSessionModel>();
// }
// export default UserSessionModelArray;

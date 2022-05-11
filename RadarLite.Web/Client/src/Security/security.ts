import Oidc, { UserManager } from "oidc-client";
import { authStore } from "@/stores/AuthStore";

const mgr: UserManager = new Oidc.UserManager({
  authority: "https://localhost:7056",
  client_id: "RadarLiteClient",
  redirect_uri: "https://localhost:7056/signin-oidc",
  response_type: "id_token token",
  scope: "openid profile NWS.Temperature",
  post_logout_redirect_uri: "https://localhost:7056/signout-callback-oidc",
  userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }), //({ store: authStore() }),
});

export default mgr;

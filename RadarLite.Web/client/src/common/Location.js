import { __decorate } from "tslib";
import { JsonProperty } from "@/Helpers/JsonMapper";
class Location {
    constructor() {
        this.id = "";
        this.name = "";
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
}
__decorate([
    JsonProperty("id")
], Location.prototype, "id", void 0);
__decorate([
    JsonProperty("name")
], Location.prototype, "name", void 0);
export default Location;
//# sourceMappingURL=Location.js.map
import { __decorate } from "tslib";
import { JsonProperty } from "@/Helpers/JsonMapper";
class ForecastPeriod {
    constructor() {
        this.detailedForecast = "";
        this.shortForecast = "";
        this.icon = "";
        this.windDirection = "";
        this.windSpeed = "";
        this.temperatureTrend = "";
        this.temperatureUnit = "";
        this.temperature = 0;
        this.isDateTime = false;
        this.endTime = Date;
        this.startTime = Date;
        this.name = "";
        this.number = 0;
    }
}
__decorate([
    JsonProperty(`detailedForecast`)
], ForecastPeriod.prototype, "detailedForecast", void 0);
__decorate([
    JsonProperty(`shortForecast`)
], ForecastPeriod.prototype, "shortForecast", void 0);
__decorate([
    JsonProperty(`icon`)
], ForecastPeriod.prototype, "icon", void 0);
__decorate([
    JsonProperty(`windDirection`)
], ForecastPeriod.prototype, "windDirection", void 0);
__decorate([
    JsonProperty(`windSpeed`)
], ForecastPeriod.prototype, "windSpeed", void 0);
__decorate([
    JsonProperty(`temperatureTrend`)
], ForecastPeriod.prototype, "temperatureTrend", void 0);
__decorate([
    JsonProperty(`temperatureUnit`)
], ForecastPeriod.prototype, "temperatureUnit", void 0);
__decorate([
    JsonProperty(`temperature`)
], ForecastPeriod.prototype, "temperature", void 0);
__decorate([
    JsonProperty(`isDateTime`)
], ForecastPeriod.prototype, "isDateTime", void 0);
__decorate([
    JsonProperty(`endTime`)
], ForecastPeriod.prototype, "endTime", void 0);
__decorate([
    JsonProperty(`startTime`)
], ForecastPeriod.prototype, "startTime", void 0);
__decorate([
    JsonProperty(`name`)
], ForecastPeriod.prototype, "name", void 0);
__decorate([
    JsonProperty(`number`)
], ForecastPeriod.prototype, "number", void 0);
export default ForecastPeriod;
//# sourceMappingURL=Period.js.map
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (g && (g = 0, op[0] && (_ = 0)), _) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
import { $MessageBox } from "@docsvision/webclient/System/$MessageBox";
var ApplicationBriefInfoLogic = /** @class */ (function () {
    function ApplicationBriefInfoLogic() {
    }
    ApplicationBriefInfoLogic.prototype.briefInfoView = function (sender) {
        return __awaiter(this, void 0, void 0, function () {
            var docNameControl, reasonControl, creationDateControl, dateTripFromControl, dateTripToControl, cityControl, docName, docNameLabel, creationDate, creationDateLabel, dateFrom, dateFromLabel, dateTo, dateToLabel, reason, reasonLabel, city, cityLable, text;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        docNameControl = sender.layout.controls.get("documentName");
                        reasonControl = sender.layout.controls.get("reason");
                        creationDateControl = sender.layout.controls.get("creationDate");
                        dateTripFromControl = sender.layout.controls.get("dateTripFrom");
                        dateTripToControl = sender.layout.controls.get("dateTripTo");
                        cityControl = sender.layout.controls.get("city");
                        docName = docNameControl.value;
                        docNameLabel = docNameControl.params.labelText;
                        creationDate = this.formatDate(creationDateControl.value);
                        creationDateLabel = creationDateControl.params.labelText;
                        dateFrom = this.formatDate(dateTripFromControl.value);
                        dateFromLabel = dateTripFromControl.params.labelText;
                        dateTo = this.formatDate(dateTripToControl.value);
                        dateToLabel = dateTripToControl.params.labelText;
                        reason = reasonControl.value;
                        reasonLabel = reasonControl.params.labelText;
                        city = cityControl.value.name;
                        cityLable = cityControl.params.labelText;
                        text = ["".concat(docNameLabel, ": ").concat(docName), "".concat(creationDateLabel, ": ").concat(creationDate), "".concat(dateFromLabel, ": ").concat(dateFrom), "".concat(dateToLabel, ": ").concat(dateTo), "".concat(reasonLabel, ": ").concat(reason), "".concat(cityLable, ": ").concat(city),
                        ].join('\n');
                        return [4 /*yield*/, sender.getService($MessageBox).showInfo(text, "Краткая информация")];
                    case 1:
                        _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    ApplicationBriefInfoLogic.prototype.formatDate = function (d) {
        if (!d)
            return "-";
        return new Intl.DateTimeFormat("ru-RU", { day: "numeric", month: "long", year: "numeric" }).format(d);
    };
    return ApplicationBriefInfoLogic;
}());
export { ApplicationBriefInfoLogic };
//# sourceMappingURL=ApplicationBriefInfoLogic.js.map
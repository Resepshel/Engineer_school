import * as ApplicationBuisnessTripHandlers from "./EventHandlers/ApplicationBuisnessTripHandlers";
import * as ApplicationDateChangeHandler from "./EventHandlers/ApplicationDateChangeHandler";
import * as ApplicationBriefInfoHandler from "./EventHandlers/ApplicationBriefInfoHandler";
import { extensionManager } from "@docsvision/webclient/System/ExtensionManager";
import { Service } from "@docsvision/webclient/System/Service";
import { $BuisnessTripAppService } from "./Services/Interfaces/IBusinessTripAppService";
import { BusinessTripAppService } from "./Services/BusinessTripAppService";
import * as BusinessTripPersonHandler from "./EventHandlers/BusinessTripPersonHandler";
import * as BusinessTripCityHandler from "./EventHandlers/BusinessTripCityHandler";
// Данный файл является входной точкой для сборки расширения.
// Он должен прямо или косвенно импортировать все другие файлы скриптов.
// Регистрируем расширение и все его обработчики
extensionManager.registerExtension({
    name: "ValidationControls",
    version: "1.0",
    globalEventHandlers: [
        ApplicationBuisnessTripHandlers,
        ApplicationDateChangeHandler,
        ApplicationBriefInfoHandler,
        BusinessTripPersonHandler,
        BusinessTripCityHandler
    ],
    layoutServices: [
        Service.fromFactory($BuisnessTripAppService, function (services) { return new BusinessTripAppService(services); }),
    ]
});
//# sourceMappingURL=Index.js.map
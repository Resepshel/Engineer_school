import * as ApplicationBuisnessTripHandlers from "./EventHandlers/ApplicationBuisnessTripHandlers";
import * as ApplicationDateChangeHandler from "./EventHandlers/ApplicationDateChangeHandler";
import * as ApplicationBriefInfoHandler from "./EventHandlers/ApplicationBriefInfoHandler";
import { extensionManager } from "@docsvision/webclient/System/ExtensionManager";

// Данный файл является входной точкой для сборки расширения.
// Он должен прямо или косвенно импортировать все другие файлы скриптов.

// Регистрируем расширение и все его обработчики
extensionManager.registerExtension({
    name: "ValidationControls",
    version: "1.0",
    globalEventHandlers: [
        ApplicationBuisnessTripHandlers,
        ApplicationDateChangeHandler,
        ApplicationBriefInfoHandler
    ]
})
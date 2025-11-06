import { $RequestManager } from "@docsvision/webclient/System/$RequestManager";
import { IBusinessTripAppService } from "./Interfaces/IBusinessTripAppService";
import { ControllerBase, HttpMethods } from "@docsvision/webclient/System/ControllerBase";
import { IBusinessTripAppNameModel } from "../Model/IBusinessTripAppNameModel";
import { IBusinessTripAppNameRequestModel } from "../Model/IBusinessTripAppNameRequestModel";

export class BusinessTripAppService extends ControllerBase implements IBusinessTripAppService {

    protected controllerName: string = "BusinessTripApp";

    constructor(protected services: $RequestManager) {
        super(services);
    }

    GetComPersInfo(model: IBusinessTripAppNameRequestModel): Promise<IBusinessTripAppNameModel> {
        return super.doRequest({
            controller: this.controllerName,
            action: 'GetComPersInfo',
            isApi: true,
            method: HttpMethods.Post,
            data: { model },
            options: { isShowOverlay: false }
        });
    }

    GetCityInfo(model: IBusinessTripAppNameRequestModel): Promise<IBusinessTripAppNameModel> {
        return super.doRequest({
            controller: this.controllerName,
            action: 'GetCityInfo',
            isApi: true,
            method: HttpMethods.Post,
            data: { model },
            options: { isShowOverlay: false }
        });
    }
}
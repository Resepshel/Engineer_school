import { serviceName } from "@docsvision/webclient/System/ServiceUtils";
import { IBusinessTripAppNameModel } from "../../Model/IBusinessTripAppNameModel";
import { IBusinessTripAppNameRequestModel } from "../../Model/IBusinessTripAppNameRequestModel";

export interface IBusinessTripAppService {
    
    GetComPersInfo(model: IBusinessTripAppNameRequestModel): Promise<IBusinessTripAppNameModel>;
    GetCityInfo(model: IBusinessTripAppNameRequestModel): Promise<IBusinessTripAppNameModel>;
}

export type $BusinessTripAppService = { activityPlanService: IBusinessTripAppService };
export const $BuisnessTripAppService = serviceName<$BusinessTripAppService, IBusinessTripAppService>(x => x.activityPlanService);
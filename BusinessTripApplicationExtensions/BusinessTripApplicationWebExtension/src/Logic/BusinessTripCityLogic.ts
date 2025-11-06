import { Layout } from "@docsvision/webclient/System/Layout";
import { DirectoryDesignerRow } from "@docsvision/webclient/BackOffice/DirectoryDesignerRow";
import { IDataChangedEventArgs } from "@docsvision/webclient/System/IDataChangedEventArgs";
import { $BuisnessTripAppService } from "../Services/Interfaces/IBusinessTripAppService";
import { DateTimePicker } from "@docsvision/webclient/Platform/DateTimePicker"; 

export class BusinessTripCityLogic {
    public async onCityChanged(sender: DirectoryDesignerRow, args: IDataChangedEventArgs) {
        const layout = sender.layout;

        const value: any = sender.value;
        let cityId: string | null = null;

        if (!value) {
            cityId = null;
        } else if (typeof value === "string") {
            cityId = value;
        } else if (value.id) {
            cityId = value.id;
        } else if (value.objectId) {
            cityId = value.objectId;
        } else if (value.getObjectId) {
            try { cityId = value.getObjectId(); } catch { cityId = null; }
        }

        var dateFrom = layout.controls.get<DateTimePicker>("dateTripFrom").value;
        var dateTo = layout.controls.get<DateTimePicker>("dateTripTo").value;

        const req = {
            cityId: cityId,
            dateFrom: dateFrom,
            dateTo: dateTo
        };

        const svc = layout.getService($BuisnessTripAppService);
        const res = await svc.GetCityInfo(req);

        const salaryCtrl = layout.controls.get<any>("salary");
        const tripDaysCtrl = layout.controls.get<any>("tripDays");
        salaryCtrl.value = res.salary;
        tripDaysCtrl.value = res.tripDays;


        //tripDaysCtrl.value = res.manager;
    }
}
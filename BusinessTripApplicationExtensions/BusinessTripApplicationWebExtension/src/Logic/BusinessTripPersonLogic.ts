import { Layout } from "@docsvision/webclient/System/Layout";
import { DirectoryDesignerRow } from "@docsvision/webclient/BackOffice/DirectoryDesignerRow";
import { IDataChangedEventArgs } from "@docsvision/webclient/System/IDataChangedEventArgs";
import { $BuisnessTripAppService } from "../Services/Interfaces/IBusinessTripAppService";
import { StaffDirectoryItems} from "@docsvision/webclient/BackOffice/StaffDirectoryItems";
import { StaffDirectory } from "@docsvision/webclient/BackOffice/StaffDirectory";

export class BusinessTripPersonLogic {
    public async onComPersChanged(sender: StaffDirectoryItems, args: IDataChangedEventArgs) {
        const layout = sender.layout;

        const value: any = sender.value;
        let employeeId: string | null = null;

        if (!value) {
            employeeId = null;
        } else if (typeof value === "string") {
            employeeId = value;
        } else if (value.id) {
            employeeId = value.id;
        } else if (value.objectId) {
            employeeId = value.objectId;
        } else if (value.getObjectId) {
            try { employeeId = value.getObjectId(); } catch { employeeId = null; }
        }

        const req = {
            employeeId: employeeId
        };

        const svc = layout.getService($BuisnessTripAppService);
        const res = await svc.GetComPersInfo(req);

        const managerCtrl = layout.controls.get<any>("comEmploye");
        //console.log(managerCtrl);
        //console.log(managerCtrl.params);
        //console.log(managerCtrl.params.value);
        const phoneCtrl = layout.controls.get<any>("phoneNumber");

        managerCtrl.params.value.displayName = res.managerName;
        phoneCtrl.value = res.phone;
    }
}
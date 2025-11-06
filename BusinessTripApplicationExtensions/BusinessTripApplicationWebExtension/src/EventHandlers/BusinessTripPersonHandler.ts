import { DirectoryDesignerRow } from "@docsvision/webclient/BackOffice/DirectoryDesignerRow";
import { IDataChangedEventArgs } from "@docsvision/webclient/System/IDataChangedEventArgs";
import { BusinessTripPersonLogic } from "../Logic/BusinessTripPersonLogic";
import { StaffDirectoryItems } from "@docsvision/webclient/BackOffice/StaffDirectoryItems";

export async function ddComPers_change(sender: StaffDirectoryItems, args: IDataChangedEventArgs) {
    if (!sender) return;
    const logic = new BusinessTripPersonLogic();
    await logic.onComPersChanged(sender, args);
}
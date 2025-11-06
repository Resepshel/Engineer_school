import { DirectoryDesignerRow } from "@docsvision/webclient/BackOffice/DirectoryDesignerRow";
import { IDataChangedEventArgs } from "@docsvision/webclient/System/IDataChangedEventArgs";
import { BusinessTripCityLogic } from "../Logic/BusinessTripCityLogic";

export async function ddCity_change(sender: DirectoryDesignerRow, args: IDataChangedEventArgs) {
    if (!sender) return;
    const logic = new BusinessTripCityLogic();
    await logic.onCityChanged(sender, args);
}
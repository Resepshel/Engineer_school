import { ICancelableEventArgs } from "@docsvision/webclient/System/ICancelableEventArgs";
import { ILayoutBeforeSavingEventArgs } from "@docsvision/webclient/System/ILayoutParams";
import { Layout } from "@docsvision/webclient/System/Layout";
import { ApplicationBuisnessTripLogic } from "../Logic/ApplicationBuisnessTripLogic";

export async function ddApplicationBusinessTrip_validation(sender: Layout, args: ICancelableEventArgs<ILayoutBeforeSavingEventArgs>) {
    if (!sender) {return; }
    let logic = new ApplicationBuisnessTripLogic();
    await logic.validationData(sender, args);
}
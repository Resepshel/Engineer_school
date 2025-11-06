import { Layout } from "@docsvision/webclient/System/Layout";
import { ApplicationBriefInfoLogic } from "../Logic/ApplicationBriefInfoLogic";

export async function ddBusinessTripBriefInfo_click(sender: Layout) {
    if (!sender) {return; }
    let logic = new ApplicationBriefInfoLogic();
    await logic.briefInfoView(sender);
}
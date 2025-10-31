import { Layout } from "@docsvision/webclient/System/Layout";
import { ApplicationBriefInfoLogic } from "../Logic/ApplicationBriefInfoLogic";

export async function ddApplicationBriefInfo_view(sender: Layout) {
    if (!sender) {return; }
    let logic = new ApplicationBriefInfoLogic();
    await logic.briefInfoView(sender);
}
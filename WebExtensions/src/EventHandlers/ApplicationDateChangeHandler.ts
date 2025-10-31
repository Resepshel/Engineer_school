import { ApplicationDateChangeLogic } from "../Logic/ApplicationDateChangeLogic";
import {DateTimePicker} from "@docsvision/webclient/Platform/DateTimePicker";


export async function ddApplicationDateChange_validation(sender: DateTimePicker) {
    if (!sender) {return; }
    let logic = new ApplicationDateChangeLogic();
    await logic.validationDate(sender);
}
import {ApplicationDateChangeLogic} from "../Logic/ApplicationDateChangeLogic";
import {DateTimePicker} from "@docsvision/webclient/Platform/DateTimePicker";
import {IDataChangedEventArgs} from "@docsvision/webclient/System/IDataChangedEventArgs";

export async function ddBusinessTripDate_change(sender: DateTimePicker, args: IDataChangedEventArgs) {
    if (!sender) {return; }
    let logic = new ApplicationDateChangeLogic();
    await logic.changeDate(sender, args);
    
}
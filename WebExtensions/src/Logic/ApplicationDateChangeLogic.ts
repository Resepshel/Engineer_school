import {DateTimePicker} from "@docsvision/webclient/Platform/DateTimePicker";
import {IDataChangedEventArgs} from "@docsvision/webclient/System/IDataChangedEventArgs";

export class ApplicationDateChangeLogic {
    public async changeDate(dateCtrl: DateTimePicker, args: IDataChangedEventArgs) {
        const layout = dateCtrl.layout;
        const datePickerFrom = layout.controls.get<DateTimePicker>("dateTripFrom");
        const datePickerTo = layout.controls.get<DateTimePicker>("dateTripTo");

        if (datePickerTo.value < datePickerFrom.value) {
            datePickerTo.value = args.oldValue;
        }
    }
}
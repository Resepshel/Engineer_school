import {DateTimePicker} from "@docsvision/webclient/Platform/DateTimePicker";

export class ApplicationDateChangeLogic {
    public async validationDate(dateCtrl: DateTimePicker) {
        const layout = dateCtrl.layout;
        const datePickerFrom = layout.controls.get<DateTimePicker>("dateTripFrom");
        const datePickerTo = layout.controls.get<DateTimePicker>("dateTripTo");

        if (datePickerTo.value < datePickerFrom.value) {
            datePickerTo.value = datePickerFrom.value
        }
    }
}
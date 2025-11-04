import { Layout } from "@docsvision/webclient/System/Layout";
import {TextBox} from "@docsvision/webclient/Platform/TextBox";
import {DateTimePicker} from "@docsvision/webclient/Platform/DateTimePicker";
import { $MessageBox } from "@docsvision/webclient/System/$MessageBox";
import { DirectoryDesignerRow} from "@docsvision/webclient/BackOffice/DirectoryDesignerRow";

export class ApplicationBriefInfoLogic {
    public async briefInfoView(sender:Layout) {
        let docNameControl = sender.layout.controls.get<TextBox>("documentName");
        let reasonControl = sender.layout.controls.get<TextBox>("reason");
        let creationDateControl = sender.layout.controls.get<DateTimePicker>("creationDate");
        let dateTripFromControl = sender.layout.controls.get<DateTimePicker>("dateTripFrom");
        let dateTripToControl = sender.layout.controls.get<DateTimePicker>("dateTripTo");
        let cityControl = sender.layout.controls.get<DirectoryDesignerRow>("city");

        let docName = docNameControl.value;
        let docNameLabel = docNameControl.params.labelText;
        let creationDate = creationDateControl.params;
        let creationDateLabel = creationDateControl.params.labelText;
        let dateFrom = dateTripFromControl.value.getFullYear();
        let dateFromLabel = dateTripFromControl.params.labelText;
        let dateTo = dateTripToControl.value.getMonth();
        let dateToLabel = dateTripToControl.params.labelText;
        let reason = reasonControl.value;
        let reasonLabel = reasonControl.params.labelText;
        let city = cityControl.value.name;
        let cityLable = cityControl.params.labelText;

        const text = [`${docNameLabel}: ${docName}`, `${creationDateLabel}: ${creationDate}`,
                `${dateFromLabel}: ${dateFrom}`, `${dateToLabel}: ${dateTo}`, `${reasonLabel}: ${reason}`,
               `${cityLable}: ${city}`,
            ].join('\n');
        
        await sender.getService($MessageBox).showInfo(text, "Краткая информация");
    }
}
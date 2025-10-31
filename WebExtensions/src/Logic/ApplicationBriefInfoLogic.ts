import { Layout } from "@docsvision/webclient/System/Layout";
import {TextBox} from "@docsvision/webclient/Platform/TextBox";
import {DateTimePicker} from "@docsvision/webclient/Platform/DateTimePicker";
import { $MessageBox } from "@docsvision/webclient/System/$MessageBox";
import { DirectoryDesignerRow} from "@docsvision/webclient/BackOffice/DirectoryDesignerRow";

export class ApplicationBriefInfoLogic {
    public async briefInfoView(sender:Layout) {
        let docName = sender.layout.controls.get<TextBox>("documentName").value;
        let docNameLabel = sender.layout.controls.get<TextBox>("documentName").params.labelText;
        let creationDate = sender.layout.controls.get<DateTimePicker>("creationDate").value;
        let creationDateLabel = sender.layout.controls.get<DateTimePicker>("creationDate").params.labelText;
        let dateFrom = sender.layout.controls.get<DateTimePicker>("dateTripFrom").value;
        let dateFromLabel = sender.layout.controls.get<DateTimePicker>("dateTripFrom").params.labelText;
        let dateTo = sender.layout.controls.get<DateTimePicker>("dateTripTo").params.value;
        let dateToLabel = sender.layout.controls.get<DateTimePicker>("dateTripTo").params.labelText;
        let reason = sender.layout.controls.get<TextBox>("reason").value;
        let reasonLabel = sender.layout.controls.get<TextBox>("reason").params.labelText;
        let city = sender.layout.controls.get<DirectoryDesignerRow>("city").params.value;
        let cityLable = sender.layout.controls.get<DirectoryDesignerRow>("city").params.labelText;

        const text = [`${docNameLabel}: ${docName}`, `${creationDateLabel}: ${creationDate}`,
                `${dateFromLabel}: ${dateFrom}`, `${dateToLabel}: ${dateTo}`, `${reasonLabel}: ${reason}`,
               `${cityLable}: ${city}`,
            ].join('\n');
        
        await sender.getService($MessageBox).showInfo(text, "Краткая информация");
    }
}
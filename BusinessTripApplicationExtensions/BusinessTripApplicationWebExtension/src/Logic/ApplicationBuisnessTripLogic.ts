import { ICancelableEventArgs } from "@docsvision/webclient/System/ICancelableEventArgs";
import { ILayoutBeforeSavingEventArgs } from "@docsvision/webclient/System/ILayoutParams";
import { Layout } from "@docsvision/webclient/System/Layout";

export class ApplicationBuisnessTripLogic {
    public async validationData(sender:Layout, args: ICancelableEventArgs<ILayoutBeforeSavingEventArgs>) {
        let validationResults = args.data?.control?.validate({ ShowErrorMessage: true }) || [];
        let invalidResults = validationResults.filter((value) => !value.Passed);
        if (invalidResults.length !== 0) {
            const text = sender.layout.params.services.locale === 'ru' ? 'Необходимо заполнить обязательные поля' : 'Required fields must be filled';
            sender.layout.params.services.messageWindow.showInfo(`${text}`);
        }
    }
}
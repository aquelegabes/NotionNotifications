import { NotificationCardModel } from "./notificationCardModel.type"

export type CalendarDayModel = {
    id: string,
    date: Date,
    label: string,
    items: NotificationCardModel[],
    cssClasses? :string
}
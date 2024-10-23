export type NotificationCardModel = {
    id: string,
    title: string,
    occurrence: string,
    date: Date,
    categories?: string[],
    alreadyNotified: boolean
}
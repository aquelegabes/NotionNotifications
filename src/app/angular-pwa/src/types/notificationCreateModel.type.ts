export type NotificationCreateModel = {
    title: string,
    occurrence: string,
    date: Date,
    categories?: string[],
    alreadyNotified: boolean
}
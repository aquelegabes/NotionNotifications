import { Injectable } from '@angular/core';
import { Observable } from "rxjs";

import {
    NotificationCardModel,
    NotificationCreateModel,
    NotificationFilter
} from "../types";


@Injectable({
    providedIn: 'root'
})
export class NotificationService {
    constructor() {

    }

    public changeNotificationStatus(
        notificationId: string, status: boolean): Observable<boolean> {
        return new Observable<boolean>()
    }

    public removeNotification(
        notificationId: string): Observable<boolean> {
        return new Observable<boolean>()
    }

    public listNotifications(
        notificationFilters: NotificationFilter): Observable<NotificationCardModel[]> {
        return new Observable<NotificationCardModel[]>()
    }

    public newNotification(
        model: NotificationCreateModel): Observable<NotificationCardModel> {
        return new Observable<NotificationCardModel>()
    }
}
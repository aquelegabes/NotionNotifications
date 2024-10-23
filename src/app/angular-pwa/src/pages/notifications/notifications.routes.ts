import { Routes } from "@angular/router";
import { NotificationsComponent } from "./notifications.component";

export const NotificationsRoutes: Routes = [
    {
        title: 'Notifications',
        component: NotificationsComponent,
        path: 'notifications'
    },
    {
        title: 'Notifications',
        component: NotificationsComponent,
        path: 'notifications/:id'
    },
]
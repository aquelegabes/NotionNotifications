import { Routes } from "@angular/router";

import { ErrorRoutes } from './error/error.routes'
import { NotificationsRoutes } from './notifications/notifications.routes'
import { SettingsRoutes } from './settings/settings.routes'
import { DashboardRoutes } from './dashboard/dashboard.routes'

export const routes: Routes = [
    ...DashboardRoutes,
    ...NotificationsRoutes,
    ...SettingsRoutes,
    ...ErrorRoutes,
]
import { Routes } from "@angular/router";
import { ErrorComponent } from "./error.component";

export const ErrorRoutes: Routes = [{
    title: 'Error',
    component: ErrorComponent,
    path: '**'
}]
import { Component } from '@angular/core';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import {
  NotificationCardListComponent,
  NotificationCardViewComponent
} from '../../components/notification-card';
import { NotificationFilter } from '../../types';
import { SearchBarComponent } from "../../components/search-bar/search-bar.component";

@Component({
  selector: 'app-dashboard-page',
  standalone: true,
  imports: [
    NavbarComponent,
    NotificationCardListComponent,
    NotificationCardViewComponent,
    SearchBarComponent
],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  upcomingFilters: NotificationFilter = {}
  latestFilters: NotificationFilter = {}

}

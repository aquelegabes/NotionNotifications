import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import {
  NotificationCardListComponent,
  NotificationCardViewComponent
} from '../../components/notification-card';
import { NotificationCardModel, NotificationFilter } from '../../types';
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
export class DashboardComponent implements OnInit {
  upcomingNotifications: NotificationCardModel[] = []
  latestNotifications: NotificationCardModel[] = []

  ngOnInit(): void {
    this.upcomingNotifications = this.loadNotifications({});
    this.latestNotifications = this.loadNotifications({});
  }

  loadNotifications(filter: NotificationFilter) {
    // TODO: get from service
    return [
      { alreadyNotified: true, date: new Date(Date.now()), id: '1', title: 'Notification 1', occurrence: 'Daily' },
      { alreadyNotified: true, date: new Date(Date.now()), id: '2', title: 'Notification 2', occurrence: 'Daily' },
      { alreadyNotified: true, date: new Date(Date.now()), id: '3', title: 'Notification 3', occurrence: 'Daily' },
      { alreadyNotified: true, date: new Date(Date.now()), id: '4', title: 'Notification 4', occurrence: 'Daily' },
    ] as NotificationCardModel[]
  }
}

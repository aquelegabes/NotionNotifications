import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PageNotificationsListComponent } from './list/list.component';
import { PageNotificationsDetailComponent } from './detail/detail.component';
import { CalendarListComponent } from "../../components/calendar-list/calendar-list.component";

@Component({
  selector: 'app-notifications-page',
  standalone: true,
  imports: [PageNotificationsDetailComponent, PageNotificationsListComponent, CalendarListComponent],
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.scss'
})
export class NotificationsComponent implements OnInit {
  notificationId?: string | null;

  constructor(private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.notificationId = this.route.snapshot.params['id']
  }
}

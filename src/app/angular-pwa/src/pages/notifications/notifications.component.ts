import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PageNotificationsListComponent } from './list/list.component';
import { PageNotificationsDetailComponent } from './detail/detail.component';
import { CalendarListComponent } from "../../components/calendar-list/calendar-list.component";
import { NotificationCardModel } from '../../types';
import { uuidv4 } from '../../utils';

@Component({
  selector: 'app-notifications-page',
  standalone: true,
  imports: [PageNotificationsDetailComponent, PageNotificationsListComponent, CalendarListComponent],
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.scss'
})
export class NotificationsComponent implements OnInit {
  notificationId?: string | null
  notificationModel: NotificationCardModel = {
      id: uuidv4(),
      title: 'title',
      date: new Date(Date.now()),
      alreadyNotified: false,
      occurrence: 'Daily'
    } as NotificationCardModel
  notificationList: NotificationCardModel[] = []

  constructor(private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.notificationId = this.route.snapshot.params['id']
    this.loadNotifications()
  }

  loadNotifications() {
    //TODO: load from service
    const holder = {
      id: uuidv4(),
      title: 'title',
      date: new Date(Date.now()),
      alreadyNotified: false,
      occurrence: 'Daily'
    } as NotificationCardModel

    if (!this.notificationId) {
      this.notificationModel = holder
    }
    else {
      this.notificationList = [holder]
    }
  }
}

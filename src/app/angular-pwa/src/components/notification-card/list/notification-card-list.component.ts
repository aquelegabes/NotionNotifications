import { Component, Input, OnInit } from '@angular/core';
import { NotificationCardModel, NotificationFilter } from '../../../types';
import { NotificationCardViewComponent } from "../view/notification-card-view.component";

@Component({
  selector: 'app-notification-card-list',
  standalone: true,
  imports: [NotificationCardViewComponent],
  templateUrl: './notification-card-list.component.html',
  styleUrl: './notification-card-list.component.scss'
})
export class NotificationCardListComponent implements OnInit {
  @Input() listTitle: string = 'List title'
  @Input() filters: NotificationFilter = {}

  notificationList: NotificationCardModel[] = [
    { alreadyNotified: true, date: new Date(Date.now()), id: '1', title: 'Notification 1', occurrence: 'Daily' },
    { alreadyNotified: true, date: new Date(Date.now()), id: '2', title: 'Notification 2', occurrence: 'Daily' },
    { alreadyNotified: true, date: new Date(Date.now()), id: '3', title: 'Notification 3', occurrence: 'Daily' },
    { alreadyNotified: true, date: new Date(Date.now()), id: '4', title: 'Notification 4', occurrence: 'Daily' },
  ]

  constructor() {

  }

  ngOnInit(): void {
    
  }

  onLoadNotifications() {
    
  }
}

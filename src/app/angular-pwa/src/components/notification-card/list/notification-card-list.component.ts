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
  @Input() listTitle: string | null = 'List title'
  @Input() notifications: NotificationCardModel[] = []  

  constructor() {

  }

  ngOnInit(): void {

  }

  removeNotificationFromList(notification: NotificationCardModel) {
    this.notifications = this.notifications.filter(not => notification.id != not.id)
  }
}

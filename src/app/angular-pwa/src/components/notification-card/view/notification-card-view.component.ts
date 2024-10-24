import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NotificationDatePipe } from '../../../pipes';
import { NotificationCardModel } from '../../../types';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-notification-card-view',
  standalone: true,
  imports: [NotificationDatePipe, RouterLink],
  templateUrl: './notification-card-view.component.html',
  styleUrl: './notification-card-view.component.scss'
})
export class NotificationCardViewComponent {
  @Input() notification: NotificationCardModel = {
    id: 'id',
    title: 'Notification title',
    occurrence: 'Notification occurrence',
    date: new Date(Date.now()),
    alreadyNotified: true
  }
  @Output() onDeleteNotification = new EventEmitter<NotificationCardModel>()

  onCheckNotification() {
    // TODO: do this on service
    console.log('check')
    this.notification.alreadyNotified = !this.notification.alreadyNotified
  }

  onRemoveNotification() {
    this.onDeleteNotification.emit(this.notification)
  }
}

import { Component, Input } from '@angular/core';
import { NotificationDatePipe } from '../../../pipes/notification-date.pipe';
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


  onCheckNotification() {
    
  }

  onRemoveNotification() {

  }
}

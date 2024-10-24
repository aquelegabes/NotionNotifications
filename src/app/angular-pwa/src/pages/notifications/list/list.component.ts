import { Component, Input } from '@angular/core';
import { NotificationCardListComponent } from '../../../components/notification-card';
import { NotificationCardModel } from '../../../types';

@Component({
  selector: 'notifications-list',
  standalone: true,
  imports: [NotificationCardListComponent],
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss'
})
export class PageNotificationsListComponent {
  @Input() listItems: NotificationCardModel[] = []
}

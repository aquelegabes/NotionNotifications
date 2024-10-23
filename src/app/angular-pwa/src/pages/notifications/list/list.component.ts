import { Component } from '@angular/core';
import { NotificationCardListComponent } from '../../../components/notification-card';

@Component({
  selector: 'notifications-list',
  standalone: true,
  imports: [NotificationCardListComponent],
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss'
})
export class PageNotificationsListComponent {

}

import { Component } from '@angular/core';
import { CalendarListComponent } from "../../components/calendar-list/calendar-list.component";
import { NotificationCardModel } from '../../types';
import { uuidv4 } from '../../utils';

@Component({
  selector: 'app-settings-page',
  standalone: true,
  imports: [CalendarListComponent],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})
export class SettingsComponent {
}

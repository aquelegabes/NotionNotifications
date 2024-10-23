import { Component } from '@angular/core';
import { CalendarListComponent } from "../../components/calendar-list/calendar-list.component";

@Component({
  selector: 'app-settings-page',
  standalone: true,
  imports: [CalendarListComponent],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})
export class SettingsComponent {

}

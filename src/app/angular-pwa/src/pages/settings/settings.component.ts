import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { CalendarListComponent } from "../../components/calendar-list/calendar-list.component";
import { SettingsModel } from '../../types';
import { locales, timezones } from '../../utils';

@Component({
  selector: 'app-settings-page',
  standalone: true,
  imports: [CalendarListComponent, FormsModule],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})
export class SettingsComponent {
  timezones = timezones()
  locales = locales()

  model: SettingsModel

  constructor() {
    this.model = {
      locale: 'pt_BR',
      timezone: 'America/Sao_Paulo'
    }
  }
}

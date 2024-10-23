import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from '../components/navbar/navbar.component';
import { SearchBarComponent } from '../components/search-bar/search-bar.component';
import { CalendarListComponent } from '../components/calendar-list/calendar-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NavbarComponent,
    SearchBarComponent,
    CalendarListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'angular-pwa';

  calendarIsShown: boolean = false;

  toggleCalendarDialog() {
    const calendarDialog: any = document.getElementById('calendar-dialog')

    if (this.calendarIsShown) {
      calendarDialog.close()
      this.calendarIsShown = false;
    }
    else {
      calendarDialog.showModal()
      this.calendarIsShown = true;
    }
  }
}

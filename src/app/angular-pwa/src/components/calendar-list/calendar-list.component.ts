import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-calendar-list',
  standalone: true,
  imports: [],
  templateUrl: './calendar-list.component.html',
  styleUrl: './calendar-list.component.scss'
})
export class CalendarListComponent implements OnInit {
  @Input() currentDate: Date = new Date(Date.now())
  @Input() locale: string = 'pt-BR';

  dayNames: string[] = []
  currentCalendar: string[][] = []

  ngOnInit(): void {
    this.currentCalendar = this.generateCalendar();
    this.dayNames = this.getDayNames(this.locale)
  }

  getDayNames(locale: string) {
    const days = [];
    const dateTimeFormat = new Intl.DateTimeFormat(locale, { weekday: 'long' });

    for (let i = 0; i < 7; i++) {
      const date = new Date(2021, 0, 3 + i);
      days.push(dateTimeFormat.format(date));
    }

    return days;
  }

  generateCalendar() {
    const year = this.currentDate.getFullYear();
    const month = this.currentDate.getMonth();
    const firstDay = new Date(year, month, 1);
    const lastDay = new Date(year, month + 1, 0);
    const daysInMonth = lastDay.getDate();
    const weekDay = firstDay.getDay();
    const lastPreviousDay = new Date(year, month, 0).getDate();

    const calendar = [];
    let week: string[] = [];

    for (let i = weekDay - 1; i >= 0; i--) {
      week.unshift((lastPreviousDay - i).toString())
    }

    for (let day = 1; day <= daysInMonth; day++) {
      week.push(day.toString());
      if (week.length === 7) {
        calendar.push(week);
        week = [];
      }
    }

    const daysInNextMonth = 7 - week.length;
    for (let day = 1; day <= daysInNextMonth; day++) {
      week.push(day.toString())
    }

    if (week.length > 0) {
      calendar.push(week);
    }

    return calendar;
  }
}

import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'calendarDate',
  standalone: true
})
export class CalendarDatePipe implements PipeTransform {

  transform(value: Date, locale: string): string {
    const dateTimeFormat = new Intl.DateTimeFormat(locale, { localeMatcher: 'best fit' })
    return dateTimeFormat.format(value)
  }
}

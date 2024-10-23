import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateMonthName',
  standalone: true
})
export class DateMonthNamePipe implements PipeTransform {

  transform(value: Date, locale: string): string {
    const dateTimeFormat = new Intl.DateTimeFormat(locale, { month: 'long' });
    return dateTimeFormat.format(value)
  }
}

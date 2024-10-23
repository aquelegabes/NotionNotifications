import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateYear',
  standalone: true
})
export class DateYearPipe implements PipeTransform {

  transform(value: Date, locale: string): string {
    const dateTimeFormat = new Intl.DateTimeFormat(locale, { year: 'numeric' });
    return dateTimeFormat.format(value)
  }
}

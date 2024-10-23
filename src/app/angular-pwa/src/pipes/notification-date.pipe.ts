import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'notificationDate',
  standalone: true
})
export class NotificationDatePipe implements PipeTransform {

  transform(value: Date): string {
    return `${value.toLocaleDateString()} - ${value.toLocaleTimeString()}`
  }

}

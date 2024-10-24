import { Component, Input } from '@angular/core';
import { NotificationCardModel } from '../../../types';
import { Occurrences, uuidv4 } from '../../../utils';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'notifications-detail',
  standalone: true,
  imports: [FormsModule, DatePipe],
  templateUrl: './detail.component.html',
  styleUrl: './detail.component.scss'
})
export class PageNotificationsDetailComponent {
  @Input() model: NotificationCardModel = {
    id: uuidv4(),
    date: new Date(Date.now()),
    alreadyNotified: false,
    title: '',
    occurrence: '',
  }

  availableOccurrences: string[] = []

  constructor() {
    for (var occ in Occurrences) {
      const isValueProperty = Number(occ) >= 0
      if (isValueProperty) {
        this.availableOccurrences.push(Occurrences[occ])
      }
    }
  }

  changeDate(evt: any) {
    const [year, month, day] = evt.split('-')
    this.model.date = new Date(year, month, day)
  }

  changeTime(evt: any) {
    const [hours, min] = evt.split(':')
    const newDateTime = new Date(this.model.date.setHours(hours, min))
    this.model.date = newDateTime
    console.log('curr model =>', this.model)
  }
}

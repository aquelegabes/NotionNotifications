import {
    Component,
    Input,
    OnInit
} from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms'

import { uuidv4 } from '../../utils';
import {
    CalendarDatePipe,
    DateMonthNamePipe,
    DateYearPipe
} from '../../pipes';
import {
    CalendarDayModel,
    NotificationCardModel
} from '../../types';
import { NotificationCardListComponent } from "../notification-card/list/notification-card-list.component";

@Component({
    selector: 'app-calendar-list',
    standalone: true,
    imports: [
        CalendarDatePipe,
        DateMonthNamePipe,
        DateYearPipe,
        DatePipe,
        FormsModule,
        NotificationCardListComponent
    ],
    templateUrl: './calendar-list.component.html',
    styleUrl: './calendar-list.component.scss'
})
export class CalendarListComponent implements OnInit {
    @Input() locale: string = 'pt-BR';
    @Input() currentDate: Date = new Date(Date.now())
    @Input() items: NotificationCardModel[] = []

    dayNames: { id: string, label: string }[] = []
    currentCalendar: CalendarDayModel[][] = []

    selectedDay: CalendarDayModel | null = null

    ngOnInit(): void {
        // TODO: remove this
        this.items = [
            {
                title: 'item 1',
                id: uuidv4(),
                date: new Date(new Date(Date.now()).setDate(25)),
                occurrence: 'Daily',
                alreadyNotified: false
            },
            {
                title: 'item 2',
                id: uuidv4(),
                date: new Date(new Date(Date.now()).setDate(23)),
                occurrence: 'Daily',
                alreadyNotified: false
            },
            {
                title: 'item 3',
                id: uuidv4(),
                date: new Date(new Date(Date.now()).setDate(23)),
                occurrence: 'Daily',
                alreadyNotified: false
            },
            {
                title: 'item 4',
                id: uuidv4(),
                date: new Date(new Date(Date.now()).setDate(11)),
                occurrence: 'Daily',
                alreadyNotified: false
            },
        ]
        this.dayNames = this.getDayNames(this.locale)
        this.currentCalendar = this.generateCalendar()
    }

    changeDate(evt: any) {
        const [year, month, day] = evt.split('-')
        this.currentDate = new Date(year, month - 1, day)
        this.currentCalendar = this.generateCalendar()
    }

    changeCurrentMonth(month: number) {
        let currYear = this.currentDate.getFullYear()

        if (month < 0) {
            month = 11;
            currYear -= 1;
        }
        if (month > 11) {
            month = 0;
            currYear += 1;
        }

        this.currentDate = new Date(new Date(this.currentDate.setMonth(month)).setFullYear(currYear))
        this.currentCalendar = this.generateCalendar();
    }

    changeCurrentYear(year: number) {
        this.currentDate = new Date(this.currentDate.setFullYear(year))
        this.currentCalendar = this.generateCalendar();
    }

    openModal(day: CalendarDayModel) {
        if (day.items?.length == 0)
            return;

        this.selectedDay = day;
        const dialog: any = document.getElementById('item-content');
        dialog.showModal();
    }

    getDayNames(locale: string) {
        const days = [];
        const dateTimeFormat = new Intl.DateTimeFormat(locale, { weekday: 'short' });

        for (let i = 0; i < 7; i++) {
            // jan 3 2021 is monday
            const date = new Date(2021, 0, 3 + i);
            days.push({
                id: uuidv4(),
                label: dateTimeFormat.format(date)
            });
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

        const calendar = [];
        let week: CalendarDayModel[] = [];

        for (let i = weekDay - 1; i >= 0; i--) {
            const currentItemDate = new Date(year, month, i - 1);
            const label = currentItemDate.getDate().toString()

            const currentItemModel = this.buildCalendarDay(label, currentItemDate)

            week.unshift(currentItemModel);
        }

        for (let day = 1; day <= daysInMonth; day++) {
            const currentItemDate = new Date(year, month, day);
            const label = currentItemDate.getDate().toString()

            const currentItemModel = this.buildCalendarDay(label, currentItemDate)

            week.push(currentItemModel);

            if (week.length === 7) {
                calendar.push(week);
                week = [];
            }
        }

        const daysInNextMonth = 7 - week.length;
        for (let day = 1; day <= daysInNextMonth; day++) {
            const currentItemDate = new Date(year, month + 1, day)
            const label = currentItemDate.getDate().toString()

            const currentItemModel = this.buildCalendarDay(label, currentItemDate)

            week.push(currentItemModel)
        }

        if (week.length > 0) {
            calendar.push(week);
        }

        return calendar;
    }

    buildCalendarDay(label: string, date: Date): CalendarDayModel {
        const availableItems = this.items.filter(_ => _.date.setHours(0, 0, 0, 0).valueOf() == date.setHours(0, 0, 0, 0).valueOf())

        const currentDateWithoutHours = new Date(new Date(
            this.currentDate.getFullYear(),
            this.currentDate.getMonth(),
            this.currentDate.getDate()
        ).setHours(0, 0, 0, 0))

        let cssClasses = 'calendar-button'
        cssClasses += currentDateWithoutHours.valueOf() == date.valueOf() ? ' today' : ''
        cssClasses += availableItems.length > 0 ? ' has-items' : ''

        return {
            id: uuidv4(),
            date: date,
            label: label.padStart(2, '0'),
            items: availableItems,
            cssClasses: cssClasses
        }
    }
}

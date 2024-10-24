import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-search-bar',
  standalone: true,
  imports: [],
  templateUrl: './search-bar.component.html',
  styleUrl: './search-bar.component.scss'
})
export class SearchBarComponent {
  @Input() toggleCalendar: () => void = () =>{}

  openCalendar() {
    this.toggleCalendar()
  }

  onSearch() {
    
  }
}

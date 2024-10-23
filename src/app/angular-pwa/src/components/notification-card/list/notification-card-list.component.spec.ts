import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotificationCardListComponent } from './notification-card-list.component';

describe('NotificationCardList', () => {
  let component: NotificationCardListComponent;
  let fixture: ComponentFixture<NotificationCardListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NotificationCardListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NotificationCardListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

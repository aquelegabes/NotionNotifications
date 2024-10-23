import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotificationCardViewComponent } from './notification-card-view.component';

describe('NotificationCardView', () => {
  let component: NotificationCardViewComponent;
  let fixture: ComponentFixture<NotificationCardViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NotificationCardViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NotificationCardViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

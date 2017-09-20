import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PraticeComponent } from './pratice.component';

describe('PraticeComponent', () => {
  let component: PraticeComponent;
  let fixture: ComponentFixture<PraticeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PraticeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PraticeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

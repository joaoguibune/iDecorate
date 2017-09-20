import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WordExpressionsComponent } from './word-expressions.component';

describe('WordExpressionsComponent', () => {
  let component: WordExpressionsComponent;
  let fixture: ComponentFixture<WordExpressionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WordExpressionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WordExpressionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

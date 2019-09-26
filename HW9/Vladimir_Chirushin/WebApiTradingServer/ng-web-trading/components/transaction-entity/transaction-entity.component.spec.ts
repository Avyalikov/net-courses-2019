import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionEntityComponent } from './transaction-entity.component';

describe('TransactionEntityComponent', () => {
  let component: TransactionEntityComponent;
  let fixture: ComponentFixture<TransactionEntityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TransactionEntityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TransactionEntityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

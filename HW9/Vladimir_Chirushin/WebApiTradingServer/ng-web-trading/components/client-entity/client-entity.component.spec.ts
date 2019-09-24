import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientEntityComponent } from './client-entity.component';

describe('ClientEntityComponent', () => {
  let component: ClientEntityComponent;
  let fixture: ComponentFixture<ClientEntityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientEntityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientEntityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

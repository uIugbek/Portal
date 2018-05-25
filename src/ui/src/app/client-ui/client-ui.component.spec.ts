import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientUIComponent } from './client-ui.component';

describe('ClientUIComponent', () => {
  let component: ClientUIComponent;
  let fixture: ComponentFixture<ClientUIComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientUIComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientUIComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FisheyeComponent } from './fisheye.component';

describe('FisheyeComponent', () => {
  let component: FisheyeComponent;
  let fixture: ComponentFixture<FisheyeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FisheyeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FisheyeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

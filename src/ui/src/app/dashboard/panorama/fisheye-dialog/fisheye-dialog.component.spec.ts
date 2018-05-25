import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FisheyeDialogComponent } from './fisheye-dialog.component';

describe('FisheyeDialogComponent', () => {
  let component: FisheyeDialogComponent;
  let fixture: ComponentFixture<FisheyeDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FisheyeDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FisheyeDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

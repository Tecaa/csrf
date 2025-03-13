import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FakeEmailComponent } from './fake-email.component';

describe('FakeEmailComponent', () => {
  let component: FakeEmailComponent;
  let fixture: ComponentFixture<FakeEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FakeEmailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FakeEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

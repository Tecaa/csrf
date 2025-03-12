import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SecureFormComponent } from './secure-endpoint.component';

describe('SecureComponentComponent', () => {
  let component: SecureFormComponent;
  let fixture: ComponentFixture<SecureFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SecureFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SecureFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

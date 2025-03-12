import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VulnerableEndpointComponent } from './vulnerable-endpoint.component';

describe('VulnerableEndpointComponent', () => {
  let component: VulnerableEndpointComponent;
  let fixture: ComponentFixture<VulnerableEndpointComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VulnerableEndpointComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VulnerableEndpointComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

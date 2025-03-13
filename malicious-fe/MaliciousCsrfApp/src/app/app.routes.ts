import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SecureEndpointComponent } from './secure-endpoint/secure-endpoint.component';
import { VulnerableEndpointComponent } from './vulnerable-endpoint/vulnerable-endpoint.component';
import { FakeEmailComponent } from './fake-email/fake-email.component';

export const routes: Routes = [
    { path: '', component: FakeEmailComponent },
    { path: 'vulnerable-call', component: VulnerableEndpointComponent },
    { path: 'secured-call', component: SecureEndpointComponent },
];

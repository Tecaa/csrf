import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SecureEndpointComponent } from './secure-endpoint/secure-endpoint.component';
import { VulnerableEndpointComponent } from './vulnerable-endpoint/vulnerable-endpoint.component';

export const routes: Routes = [
    { path: '', component: AppComponent },
    { path: 'vulnerable-call', component: VulnerableEndpointComponent },
    { path: 'secured-call', component: SecureEndpointComponent },
];

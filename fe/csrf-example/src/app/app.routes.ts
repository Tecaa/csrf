import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { TransactionsComponent } from './components/transactions/transactions.component';

export const routes: Routes = [
    { path: '', component: LoginComponent },
    { path: 'transactions', component: TransactionsComponent }
];

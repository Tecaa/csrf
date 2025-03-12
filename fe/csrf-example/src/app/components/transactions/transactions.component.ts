import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { environment } from '../../../environments/environment.dev';
import { CommonModule, NgFor } from '@angular/common';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css',
  standalone: true,
  imports: [FormsModule, CommonModule, NgFor]
})
export class TransactionsComponent implements OnInit {
  private apiUrl = environment.apiUrl;

  transactions: { id: number; amount: number; destinationAccount: string; }[] = [];

  newTransaction = {
    id: 0,
    amount: 0,
    destinationAccount: '',
  };

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.fetchTransactions();
  }

  fetchTransactions() {
    this.http.get(`${this.apiUrl}/transaction/list`)
      .subscribe(response => {
        this.transactions = response as { id: number; date: string; amount: number; destinationAccount: string; }[];
      });
  }

  addTransaction() {
    const formData = new FormData();
    formData.append('amount', this.newTransaction.amount.toString());
    formData.append('destinationAccount', this.newTransaction.destinationAccount);

    this.http.post(`${this.apiUrl}/transaction/create`, formData, { withCredentials: true }).subscribe(response => {
      window.location.reload();
    });
  }
}
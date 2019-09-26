import { Component, OnInit } from '@angular/core';
import { Transaction } from '../../models/Transaction';
import { TransactionService } from '../../services/transaction.service';
 

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})

export class TransactionsComponent implements OnInit {
  transactions: Transaction[];

  constructor(private transactionService: TransactionService) { }

  ngOnInit() {
    this.transactionService.getTransactions().subscribe(transactions => {
      this.transactions = transactions;
    });
  }
  selectTransactions(clientName: string) {
    this.transactions = this.transactions.filter(t => t.sellerName === clientName || t.buyerName === clientName);
  }
}

import { Component, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-select-transaction',
  templateUrl: './select-transaction.component.html',
  styleUrls: ['./select-transaction.component.css']
})

export class SelectTransactionComponent implements OnInit {
  @Output() selectTransactions: EventEmitter<any> = new EventEmitter();

  clientName: string;

  constructor() { }

  ngOnInit() {
  }

  onSubmit() {
    this.selectTransactions.emit(this.clientName);
  }
}

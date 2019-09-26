import { Component, OnInit, Input } from '@angular/core';
import { Transaction } from '../../models/Transaction';
import { __importDefault } from 'tslib';

@Component({
  selector: 'app-transaction-entity',
  templateUrl: './transaction-entity.component.html',
  styleUrls: ['./transaction-entity.component.css']
})
export class TransactionEntityComponent implements OnInit {
  @Input() transaction: Transaction;

  constructor() { }

  ngOnInit() {
  }

  setClasses() {
    let classes = {
      transaction: true
    }

    return classes;
  }
}

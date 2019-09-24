import { Component, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-add-client',
  templateUrl: './add-client.component.html',
  styleUrls: ['./add-client.component.css']
})
export class AddClientComponent implements OnInit {
@Output() addClient: EventEmitter<any> = new EventEmitter();

  name: string;
  phoneNumber: string;
  balance: number;

  constructor() { }

  ngOnInit() {
  }

  onSubmit() {
    const client = {
      name: this.name,
      phoneNumber: this.phoneNumber,
      balance: this.balance
    }

    this.addClient.emit(client);
  }
}

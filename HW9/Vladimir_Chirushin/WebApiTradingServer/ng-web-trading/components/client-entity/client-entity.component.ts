import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Client } from '../../models/Client';
import { ClientService } from '../../services/client.service';

@Component({
  selector: 'app-client-entity',
  templateUrl: './client-entity.component.html',
  styleUrls: ['./client-entity.component.css']
})
export class ClientEntityComponent implements OnInit {
  @Input() client: Client;
  @Output() deleteClient: EventEmitter<Client> = new EventEmitter();

  constructor(private clientService: ClientService) { }

  ngOnInit() {
  }

  setClasses() {
    let classes = {
      client: true,
      'is-green': this.client.balance > 0
    }

    return classes;
  }

  onDelete(client){
    this.deleteClient.emit(client);
  }
}

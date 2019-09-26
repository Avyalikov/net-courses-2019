import { Component, OnInit } from '@angular/core';
import { Client } from '../../models/Client';
import { ClientService } from '../../services/client.service';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {
  clients: Client[];

  constructor(private clientService: ClientService) { }

  ngOnInit() {
    this.clientService.getClients().subscribe(clients => {
        this.clients = clients;
      });
  }

  deleteClient(client: Client) {
    this.clients = this.clients.filter(c => c.name !== client.name);
    this.clientService.deleteClient(client).subscribe();
  }

  addClient(client: Client) {
    this.clientService.addClient(client).subscribe();
    this.clients.push(client);
  }
}

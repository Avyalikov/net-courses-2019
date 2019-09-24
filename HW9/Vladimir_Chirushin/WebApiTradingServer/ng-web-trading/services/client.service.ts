import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Client } from '../models/Client';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ClientService {
  clientsReadUrl: string = 'http://localhost/clients?top=100&page=1';
  clientsDeleteUrl: string = 'http://localhost/clients/remove/';
  clientAddurl: string = 'http://localhost/clients/add/';

  constructor(private http: HttpClient) { }

  getClients(): Observable<Client[]>{
    return this.http.get<Client[]>(this.clientsReadUrl);
  }

  deleteClient(client: Client): Observable<any> {
    return this.http.post<Client>(this.clientsDeleteUrl, client, httpOptions);
  }

  addClient(client: Client): Observable<Client> {
    return this.http.post<Client>(this.clientAddurl, client, httpOptions);
  }
}

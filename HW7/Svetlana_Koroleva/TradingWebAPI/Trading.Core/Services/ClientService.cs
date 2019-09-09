// <copyright file="Client.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Services

{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;
    using Trading.Core.Repositories;
    using Trading.Core.DTO;
    using Trading.Core.IServices;

    /// <summary>
    /// Client description
    /// </summary>
    public class ClientService:IClientService
    {
       
        private readonly IUnitOfWork unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public void AddClientToDB(ClientInfo args)
        {
            var clientToAdd = new Client()
            {
                FirstName = args.FirstName,
                LastName = args.LastName,
                Phone = args.Phone,
                Balance = args.Balance,
                RegistrationDateTime = DateTime.Now
            };

           if (this.unitOfWork.Clients.Get(c=>c.FirstName==args.FirstName&&c.LastName==args.LastName&&c.Phone==args.Phone).Count()!=0) {
                throw new ArgumentException("This client exists. Can't continue");
            };
            this.unitOfWork.Clients.Add(clientToAdd);
            this.unitOfWork.Save();
        }


      public Client GetEntityByID(int clientId)
        {

           if (this.unitOfWork.Clients.Get(c=>c.ClientID==clientId).Count()==0)
            {
                throw new ArgumentException("Client  doesn't exist");
            }
           var client=this.unitOfWork.Clients.Get(c=>c.ClientID==clientId).Single();
            return client;
        }



       public void EditClientBalance(int clientId, decimal sumToAdd)
        {
            var client = this.GetEntityByID(clientId);
            client.Balance += sumToAdd;
            unitOfWork.Clients.Update(client);
            unitOfWork.Save();
        }


        public IEnumerable<Client> GetClientsFromOrangeZone()
        {

            return this.unitOfWork.Clients.Get(o => o.Balance == 0);
        }

        public IEnumerable<Client> GetClientsFromBlackZone()
        {

            return this.unitOfWork.Clients.Get(o => o.Balance <= 0);
        }

        public void Delete(int clientId)
        {
           
            var clientToDelete = this.GetEntityByID(clientId);
            this.unitOfWork.Clients.Delete(clientToDelete);
            this.unitOfWork.Save();
        }

        public void Update(int clientId, ClientInfo clientinfo)
        {

            var clientToUpdate = this.GetEntityByID(clientId);
            clientToUpdate.FirstName = clientinfo.FirstName;
            clientToUpdate.LastName = clientinfo.LastName;
            clientToUpdate.Phone = clientinfo.Phone;
            clientToUpdate.Balance = clientinfo.Balance;
            this.unitOfWork.Clients.Update(clientToUpdate);
            this.unitOfWork.Save(); 
        }

        public IEnumerable<Client> GetAllClients()
        {
          return  this.unitOfWork.Clients.GetAll();
        }

        public IEnumerable<Client> GetTopClients(int amount)
        {
            return this.GetAllClients().Take(amount);
        }


        public decimal GetClientBalance(int clientId)
        {
            return this.GetEntityByID(clientId).Balance;
        }

        public string GetClientStatus(int clientId)
        {
            string status=null;
            if (this.GetClientBalance(clientId) > 0)
            {
                status="green";
            }
            if (this.GetClientBalance(clientId) < 0)
            {
                status= "black";
            }
            if (this.GetClientBalance(clientId) == 0)
            {
                status= "orange";
            }
            return status;
        }
    }

}
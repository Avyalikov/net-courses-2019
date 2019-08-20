﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;
using Traiding.Core.Repositories;

namespace Traiding.Core.Services
{
    public class SharesNumbersService
    {
        private ISharesNumberTableRepository tableRepository;

        public SharesNumbersService(ISharesNumberTableRepository sharesNumberTableRepository)
        {
            this.tableRepository = sharesNumberTableRepository;
        }

        public int Create(SharesNumberRegistrationInfo args)
        {
            var entityToAdd = new SharesNumberEntity()
            {
                Client = args.Client,
                Share = args.Share,
                Number = args.Number               
            };

            if (this.tableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("Share number with this share and client has been registered. Can't continue.");
            }

            this.tableRepository.Add(entityToAdd);

            this.tableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public void ContainsById(int entityId)
        {
            if (!this.tableRepository.ContainsById(entityId))
            {
                throw new ArgumentException("Can't find shares number of client by this Id. May it has not been registered.");
            }
        }

        public SharesNumberEntity Get(int entityId)
        {
            ContainsById(entityId);

            return this.tableRepository.Get(entityId);
        }

        public void ChangeNumber(int entityId, int newNumber)
        {
            ContainsById(entityId);

            this.tableRepository.ChangeNumber(entityId, newNumber);
        }

        public IEnumerable<SharesNumberEntity> GetByClient(int clientId)
        {
            return this.tableRepository.GetByClient(clientId);
        }

        public IEnumerable<SharesNumberEntity> GetByType(int shareTypeId)
        {
            return this.tableRepository.GetByType(shareTypeId);
        }

        public void Remove(int entityId)
        {
            ContainsById(entityId);

            this.tableRepository.Remove(entityId);
        }
    }
}
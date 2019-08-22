namespace Traiding.Core.Services
{
    //using System;
    //using System.Collections.Generic;
    //using System.Linq;
    //using System.Text;
    //using System.Threading.Tasks;
    //using Traiding.Core.Dto;
    //using Traiding.Core.Models;
    //using Traiding.Core.Repositories;

    public class BlockedMoneysService
    {
        /* Implemented in Sale service
         */
        //private IBlockedMoneyTableRepository tableRepository;

        //public BlockedMoneysService(IBlockedMoneyTableRepository blockedMoneyTableRepository)
        //{
        //    this.tableRepository = blockedMoneyTableRepository;
        //}

        //public int Create(BlockedMoneyRegistrationInfo args)
        //{
        //    var entityToAdd = new BlockedMoneyEntity()
        //    {
        //        CreatedAt = DateTime.Now,
        //        ClientBalance = args.ClientBalance,
        //        Operation = args.Operation,
        //        Customer = args.ClientBalance.Client,
        //        Total = args.Total
        //    };

        //    if (this.tableRepository.Contains(entityToAdd))
        //    {
        //        throw new ArgumentException("Bloked money with this data has been registered. Can't continue.");
        //    }

        //    this.tableRepository.Add(entityToAdd);

        //    this.tableRepository.SaveChanges();

        //    return entityToAdd.Id;
        //}
        //public void ContainsById(int entityId)
        //{
        //    if (!this.tableRepository.ContainsById(entityId))
        //    {
        //        throw new ArgumentException("Can't find bloked money with this Id. May it has not been registered.");
        //    }
        //}

        //public BlockedMoneyEntity Get(int entityId)
        //{
        //    ContainsById(entityId);

        //    return this.tableRepository.Get(entityId);
        //}

        //public void Remove(int entityId)
        //{
        //    ContainsById(entityId);

        //    this.tableRepository.Remove(entityId);

        //    this.tableRepository.SaveChanges();
        //}
    }
}

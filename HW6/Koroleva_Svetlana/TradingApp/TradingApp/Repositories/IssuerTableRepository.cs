// <copyright file="IssuerTableRepository.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;
    using Trading.Core.Repositories;
    using TradingApp.DAL;
    using Trading.Core.DTO;

    /// <summary>
    /// IssuerTableRepository description
    /// </summary>
    public class IssuerTableRepository<TEntity> : CommonTableRepositoty<TEntity> where TEntity:Issuer
    {
        public IssuerTableRepository(ExchangeContext db) : base(db)
        {
        }
        
        public override IEnumerable<TEntity> FindEntitiesByRequest(params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> FindEntitiesByRequestDTO(object DTOarguments)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> OrderById(object i)
        {
            throw new NotImplementedException();
        }
        public override TEntity GetElementAt(int position)
        {
            return (TEntity)this.db.Issuers.OrderBy(c => c.IssuerID).Skip(position - 1).Take(1).Single();

        }

        public override bool ContainsDTO(object entity)
        {
            Issuer issuer = (Issuer)entity;

            return

                this.db.Issuers
                .Any(c => c.CompanyName == issuer.CompanyName &&
                c.Address == issuer.Address 
                );
        }
        /*   public override bool Contains(object entity)
  {
      Issuer Issuer = (Issuer)entity;

      return

          this.db.Issuers
          .Any(c => c.CompanyName== Issuer.CompanyName &&
          c.Address == Issuer.Address);
  }

  public override bool ContainsByPK(params object[] pk)
  {
      int primaryKey = (int)pk[0];
      return this.db.Issuers.Any(c => c.IssuerID == primaryKey);
  }

  public override int Count()
  {
      return this.db.Issuers.Count();
  }

  public override object Find(params object[] key)
  {
      return db.Issuers.Find(key);
  }

  public override IEnumerable<object> FindEntitiesByRequest(params object[] arguments)
  {
      throw new NotImplementedException();
  }

  public override IEnumerable<object> FindEntitiesByRequestDTO(object arguments)
  {
      throw new NotImplementedException();
  }



  public override object GetElementAt(int position)
  {
      return this.db.Issuers.OrderBy(c => c.IssuerID).Skip(position - 1).Take(1).Single();
  }

  public override object OrderById(int type)
  {
      if (type == 0)
      {
          return this.db.Issuers.OrderBy(c => c.IssuerID);
      }
      return this.db.Issuers.OrderByDescending(c => c.IssuerID);
  }

  public override IEnumerable<object> Where(params object[] arguments)
  {
      //for IssuerID
      int issuerId = (int)arguments[0];
      return db.Issuers.Where(c => c.IssuerID == issuerId);
  }*/
    }
}

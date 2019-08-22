// <copyright file="IssuerService1.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Services
{
    using System;
    using Trading.Core.Model;
    using Trading.Core.Repositories;
    using Trading.Core.DTO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// IssuerService1 description
    /// </summary>
    public class IssuerService
    {
        private readonly ITableRepository<Issuer> tableRepository;
        public IssuerService(ITableRepository<Issuer> tableRepository)
        {
            this.tableRepository = tableRepository;
        }
        public void AddIssuer(IssuerInfo args)
        {
            Issuer issuer = new Issuer()
            {
                CompanyName = args.CompanyName,
                Address = args.Address
            };
            if (this.tableRepository.ContainsDTO(issuer))
            {
                throw new ArgumentException("This issuer exists. Can't continue");
            };
            tableRepository.Add(issuer);
            tableRepository.SaveChanges();

        }
               
    }
}

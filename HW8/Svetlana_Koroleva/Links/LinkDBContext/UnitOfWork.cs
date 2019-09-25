// <copyright file="UnitOfWork.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace LinkDBContext
{
    using LinkDBContext.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UrlLinksCore;
    using UrlLinksCore.Repository;

    /// <summary>
    /// UnitOfWork description
    /// </summary>
    public class UnitOfWork:IUnitOfWork
    {
        private LinksContext db= new LinksContext();

        private ILinkRepository linkRepository;
       
        public UnitOfWork()
        {
           
        }

        public ILinkRepository Links
        {
            get
            {
                if (linkRepository == null)
                {
                    linkRepository = new LinkRepository(db);
                }
                return linkRepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

    
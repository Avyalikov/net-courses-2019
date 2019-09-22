using ClimateDBContext;
using ClimateDBContext.DAL;
using Microsoft.AspNet.OData;
using ClimateDBContext.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebAppWithOData.ODataControllers
{
    public abstract class GenericController<TEntity> : ODataController where TEntity : class
    {

        protected ClimateDbContext dbContext = new ClimateDbContext();
        private DbSet dbSet;

        [EnableQuery]
        public IQueryable<TEntity> Get()
        {
            dbSet = dbContext.Set<TEntity>();
            return (IQueryable<TEntity>)dbSet;
        }

        [EnableQuery]
        public TEntity Get([FromODataUri] int key)
        {
            dbSet = dbContext.Set<TEntity>();
            var entity=(TEntity)dbSet.Find(key);
            return entity;
        }
        
        public async Task<IHttpActionResult> Post(TEntity entity)
        {
            dbSet = dbContext.Set<TEntity>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dbSet.Add(entity);
            await dbContext.SaveChangesAsync();
            return Created(entity);
        }

        private bool EntityExists(int id)
        {
            bool exists = false;
            if (Get(id) != null)
            {
                exists = true;
                return exists;
            }
            return exists;
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<TEntity> delta)
        {
            dbSet = dbContext.Set<TEntity>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await dbSet.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            delta.Patch((TEntity)entity);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(entity);
        }

        public abstract bool IdCheck(int id, TEntity entity);

        public async Task<IHttpActionResult> Put([FromODataUri] int key, TEntity entityToUpdate)
        {
            dbSet = dbContext.Set<TEntity>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (IdCheck(key, entityToUpdate) == false)
            {
                return BadRequest();
            }

            dbContext.Entry(entityToUpdate).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(entityToUpdate);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            dbSet = dbContext.Set<TEntity>();
            var entity = await dbSet.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            dbSet.Attach(entity);
            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}
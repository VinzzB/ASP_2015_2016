using Asp_Mvc_2015_2016.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Asp_Mvc_2015_2016.DAL
{
    public class GenericRepository<T> where T : class
    {
        internal FacturatieDBContext context;
        internal DbSet<T> DbSet {get; set; }

        public GenericRepository() : this(new FacturatieDBContext())
        { }

        public GenericRepository(FacturatieDBContext context)
        {
            this.context = context;
            DbSet = context.Set<T>();
        }

        public T GetById(int? id)
        {
            return  DbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            //adds and sends status modified to the context met onderstaande code
            context.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        ////Save changes en dispose gaan naar unit of work class
        //public void SaveChanges()
        //{
        //    context.SaveChanges();
        //}

        //public void Dispose()
        //{
        //    if (!disposed)
        //    {
        //        context.Dispose();
        //        disposed = true;
        //    }
        //}
    }
     
}
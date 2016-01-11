using Asp_Mvc_2015_2016.Models;
using Asp_Mvc_2015_2016.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Asp_Mvc_2015_2016.DAL
{
    /// <summary>
    /// A Generic repository that holds data with an INTEGER as Unique Key.
    /// </summary>
    /// <typeparam name="T">The dataobject</typeparam>
    public class GenericRepository<T> : GenericRepository<T, int> where T : class
    {
        public GenericRepository(FacturatieDBContext context) : base(context)
        { }
    }; 

    /// <summary>A generic repository.</summary>
    /// <typeparam name="T">T is the EF model</typeparam>
    /// <typeparam name="UID">UID specifies the Unique Key Type.</typeparam>
    public class GenericRepository<T, UID> where T : class
    {
        internal FacturatieDBContext context;
        internal DbSet<T> DbSet {get; set; }

        //public GenericRepository() : this(new FacturatieDBContext())
        //{ }

        public GenericRepository(FacturatieDBContext context)
        {
            this.context = context;
            DbSet = context.Set<T>();
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>(); //.GeregistreerdeUren;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public T GetById(UID id)
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
           // context.Entry(entity).State = EntityState.Added;
           //context.Entry<T>(entity).State = EntityState.Added;
        }

        public void Update(T entity)
        {
            //adds and sends status modified to the context met onderstaande code
            context.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Delete(UID id)
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
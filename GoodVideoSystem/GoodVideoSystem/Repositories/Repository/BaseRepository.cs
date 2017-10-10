using GoodVideoSystem.Repository.IRepository;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ObjectBuilder2;
using System.Data.Entity.Validation;

namespace GoodVideoSystem.Models.Repository
{
    public abstract class BaseRepository<T>:IRepository<T> where T :class
    {
        public BaseDbContext DbContext { get; set; }
        public DbSet<T> DbSet { get; set; }

        public BaseRepository(BaseDbContext context)
        {
            Guard.ArgumentNotNull(context,"context");
            this.DbContext = context;
            this.DbSet = this.DbContext.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            return this.DbSet.AsQueryable();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter)
        {
            return this.DbSet.Where(filter).AsQueryable();
        }

        public IEnumerable<T> Get<Key>(Expression<Func<T, bool>> filter,int pageIndex,int pageSize,
            Expression<Func<T, Key>> sort,bool isAsc = true)
        {
            Guard.ArgumentNotNull(filter, "filter");
            Guard.ArgumentNotNull(sort, "sort");
            if (isAsc)
            {
                return this.DbSet.Where(filter)
                    .OrderBy(sort)
                    .Skip(pageSize * (pageSize - 1))
                    .Take(pageSize)
                    .AsQueryable();
            }
            else {
                return this.DbSet.Where(filter)
                    .OrderByDescending(sort)
                    .Skip(pageSize * (pageSize - 1))
                    .Take(pageSize)
                    .AsQueryable();
            }
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Where(predicate).Count();
        }

        public void Add(T instance)
        {
            try
            {
                Guard.ArgumentNotNull(instance, "instance");
                this.DbSet.Attach(instance);
                this.DbContext.Entry(instance).State = EntityState.Added;
                this.DbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                return;
            }
        }

        public void Update(T instance)
        {
            try
            {
                Guard.ArgumentNotNull(instance, "instance");
                this.DbSet.Attach(instance);
                this.DbContext.Entry(instance).State = EntityState.Modified;
                this.DbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                return;
            }
        }

        public void Delete(T instance)
        {
            Guard.ArgumentNotNull(instance, "instance");
            this.DbSet.Attach(instance);
            this.DbContext.Entry(instance).State = EntityState.Deleted;
            this.DbContext.SaveChanges();
        }

        public void Dispose()
        {
            this.DbContext.Dispose();
        }
    }
}
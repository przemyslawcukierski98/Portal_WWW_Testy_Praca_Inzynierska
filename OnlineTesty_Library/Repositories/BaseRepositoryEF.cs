using Microsoft.EntityFrameworkCore;
using OnlineTesty_Library.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library.Repositories
{
    public class BaseRepositoryEF
    {
        protected IUnitOfWork UnitOfWork { get; set; }
        protected EFDatabaseContext Context
        {
            get { return (EFDatabaseContext)this.UnitOfWork; }
        }

        public BaseRepositoryEF(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("UnitOfWork");
            this.UnitOfWork = unitOfWork;
        }

        protected virtual DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return this.Context.Set<TEntity>();
        }

        protected virtual void SetEntityState(object entity, EntityState entityState)
        {
            this.Context.Entry(entity).State = entityState;
        }
    }
}

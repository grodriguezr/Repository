using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace EFRepository
{
    public class Repository : IRepository, IDisposable
    {
        protected DbContext Context;//contexto general

        public Repository(DbContext context, bool autoDetectChanges = false, bool proxyCreationEnabled = false)
        {
            this.Context = context;
            this.Context.Configuration.AutoDetectChangesEnabled = autoDetectChanges;
            this.Context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
        }
         
        public TEntity Create<TEntity>(TEntity newEntity) where TEntity : class
        {
            TEntity Result = null;

            try
            {
                Result = Context.Set<TEntity>().Add(newEntity);
                TrySaveChanges();
            }
            catch (Exception e)
            {
                throw(e);
            }


            return Result;
        }

        protected virtual int TrySaveChanges()//virtual permite que el método pueda ser sobreescrito
        {
            return Context.SaveChanges();//retorna un número entero que serán la cantidad de cambios realizados

        }

        public bool Delete<TEntity>(TEntity deletedEntity) where TEntity : class
        {
            bool Result = false;

            try
            {
                Context.Set<TEntity>().Attach(deletedEntity);//Hace que la entida sea vigilada por el entity framework para detectar si cambia
                Context.Set<TEntity>().Remove(deletedEntity);//se borra la entidad
                Result = TrySaveChanges() > 0;//retorna true si TrySaveChanges retorna un número mayor que 0
            }
            catch (Exception e)
            {
                throw (e);
            }


            return Result;
        }

        public TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            TEntity Result = null;

            try
            {
                Result = Context.Set<TEntity>().FirstOrDefault(criteria);
            }
            catch (Exception e)
            {

                throw(e);
            }

            return Result;
        }

        public IEnumerable<TEntity> FindEntitySet<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            List<TEntity> Result = null;

            try
            {
                Result = Context.Set<TEntity>().Where(criteria).ToList();
            }
            catch (Exception e)
            {

                throw (e);
            }

            return Result;
        }

        public bool Update<TEntity>(TEntity modifiedEntity) where TEntity : class
        {
            bool Result = false;

            try
            {
                Context.Set<TEntity>().Attach(modifiedEntity);//Hace que la entida sea vigilada por el entity framework para detectar si cambia
                Context.Entry<TEntity>(modifiedEntity).State = EntityState.Modified;//se modifica el estado de la entidad para que el entity framework haga el cambio
                Result = TrySaveChanges() > 0;//retorna true si TrySaveChanges retorna un número mayor que 0
            }
            catch (Exception e)
            {
                throw (e);
            }


            return Result;
        }

        public void Dispose()
        {
            if(Context != null)
            {
                Context.Dispose();
            }
        }
    }
}

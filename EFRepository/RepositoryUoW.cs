using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRepository
{
    public class RepositoryUoW : Repository, IRepository, IUnitOfWork, IDisposable
    {
        public RepositoryUoW(DbContext context,
            bool autoDetectChanges = false,
            bool proxyCreationEnabled = false):base(context, autoDetectChanges, proxyCreationEnabled)
        {
            this.Context = context;
            this.Context.Configuration.AutoDetectChangesEnabled = autoDetectChanges;
            this.Context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
        }

        protected override int TrySaveChanges()
        {
            return 0;
        }

        int IUnitOfWork.Save()
        {
            int Result = 0;
            try
            {
                Result = Context.SaveChanges();

            }
            catch (Exception e)
            {

                throw(e);
            }
            return Result;
        }
    }
}

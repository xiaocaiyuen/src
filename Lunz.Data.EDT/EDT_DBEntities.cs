using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunz.Data.EDT
{
    partial class EDT_DBEntities
    {
        public override int SaveChanges()
        {
            try
            {
                this.AuditEntity();

                return base.SaveChanges();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            this.AuditEntity();

            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            this.AuditEntity();

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

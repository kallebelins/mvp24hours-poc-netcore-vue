using CustomerAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Mvp24Hours.Infrastructure.Data;

namespace CustomerAPI.Infrastructure.Data
{
    public class CustomerDBContext : Mvp24HoursContext
    {
        #region [ Ctor ]

        public CustomerDBContext()
            : base()
        {
        }

        public CustomerDBContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override bool CanApplyEntityLog => true;

        protected override object EntityLogBy => "myusercontrol";

        #endregion

        #region [ Sets ]

        public virtual DbSet<Customer> Customer { get; set; }

        #endregion
    }
}

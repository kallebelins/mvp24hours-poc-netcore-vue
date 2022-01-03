using CustomerAPI.Core.Entities;
using CustomerAPI.Infrastructure.Contract;
using Microsoft.EntityFrameworkCore;
using Mvp24Hours.Infrastructure.Data.EFCore;
using System;
using System.Linq;
using System.Reflection;

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

        #endregion

        #region [ Overrides ]

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                ApplyMappingsFromAssembly(assembly, builder);
            }
            base.OnModelCreating(builder);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly, ModelBuilder builder)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IModelConfiguration<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Apply");
                methodInfo?.Invoke(instance, new object[] { builder.Entity<Customer>() });
            }
        }

        #endregion

        #region [ Sets ]

        public virtual DbSet<Customer> Customer { get; set; }

        #endregion
    }
}

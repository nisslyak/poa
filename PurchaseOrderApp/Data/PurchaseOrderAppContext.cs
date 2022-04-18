#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PurchaseOrderApp.Models;

namespace PurchaseOrderApp.Data
{
    public class PurchaseOrderAppContext : DbContext
    {
        public PurchaseOrderAppContext(DbContextOptions<PurchaseOrderAppContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<LineItem> LineItem { get; set; }
    }
}

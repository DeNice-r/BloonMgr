using System.Data.Entity;


namespace BloonMgr
{
    class AppContext : DbContext
    {
        public DbSet<OrderEntry> OrderEntries;

        public AppContext() : base("OEConnection") { }
    }
}

using CustomerOrderRESTService.EFLayer.DataAccess;

namespace CustomerOrderRESTService.Tests.BusinessLayer
{
    public class DataContextTests : DataContext
    {
        public DataContextTests(bool keepExistingDB = false) : base("Test")
        {
            if (keepExistingDB)
            {
                Database.EnsureCreated();
            }
            else
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
    }
}
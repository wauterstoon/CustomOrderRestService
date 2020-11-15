using CustomerOrderRESTService.EFLayer.DataAccess;

namespace CustomerOrderRESTService.EFLayer.Repositories
{
    public class Repository
    {
        protected readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }
    }
}
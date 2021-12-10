using DataAccessLayer;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Infrastructure
{
    public class UoWFactory : IUoWFactory
    {
        public IUnitOfWork CreateUoW()
        {
            return new UnitOfWork();
        }
    }
}

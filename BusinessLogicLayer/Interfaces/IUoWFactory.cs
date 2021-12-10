using DataAccessLayer;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUoWFactory
    {
        IUnitOfWork CreateUoW();
    }
}

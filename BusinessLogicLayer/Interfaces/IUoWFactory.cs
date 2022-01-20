using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUoWFactory
    {
        IUnitOfWork CreateUoW();
    }
}

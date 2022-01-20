using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUoWFactory uowFactory, IMapper mapper)
        {
            _unitOfWork = uowFactory.CreateUoW();
            _mapper = mapper;
        }

        public async Task<RoleDTO> GetRoleAsync(int id)
        {
            var Role = await _unitOfWork.Role.GetByIdAsync(id);

            return _mapper.Map<RoleDTO>(Role);
        }

        public async Task<List<RoleDTO>> GetRolesAsync()
        {
            var roles = await _unitOfWork.Role.GetAllAsync();

            return _mapper.Map<List<RoleDTO>>(roles);
        }

        //public async Task<List<RoleDTO>> GetRolesByGroupIdAsync(int groupId)
        //{
        //    var Roles = await _unitOfWork.Role.GetWhereAsync(d => d.GroupId == groupId);

        //    return _mapper.Map<List<RoleDTO>>(Roles);
        //}

        public async Task<bool> AnyRolesAsync(Expression<Func<Role, bool>> expression)
        {
            return await _unitOfWork.Role.AnyExistingAsync(expression);
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) _unitOfWork.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
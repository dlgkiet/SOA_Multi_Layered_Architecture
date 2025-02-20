using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.ServiceLayer
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;

        // Constructor - Dependency Injection
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        // Lấy danh sách tất cả người dùng
        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }

        // Lấy người dùng theo ID
        public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            return await _userRepository.GetByIdAsync(id, cancellationToken);
        }

        // Thêm một người dùng mới
        public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await _userRepository.AddAsync(user, cancellationToken);
        }

        // Cập nhật thông tin người dùng
        public async Task<User?> UpdateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await _userRepository.UpdateAsync(user, cancellationToken);
        }

        // Xóa một người dùng theo ID
        public async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            return await _userRepository.DeleteAsync(id, cancellationToken);
        }
    }
}

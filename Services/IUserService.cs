using AspNetCoreTodo.Models;
using System;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Services
{
    public interface IUserService
    {
        Task<Avatar> GetAvatarAsync(ApplicationUser user);

        Task<bool> SetAvatarAsync(Avatar newAvatar, ApplicationUser user);
    }
}

using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Avatar> GetAvatarAsync(ApplicationUser user)
        {
            var avatar = await _context.Avatars.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (avatar == null)
                return new Avatar { Base64 = string.Empty, UserId = user.Id };

            return avatar;
        }
        
        public async Task<bool> SetAvatarAsync(Avatar newAvatar, ApplicationUser user)
        {
            newAvatar.UserId = user.Id;

            var oldAvatar = await _context.Avatars.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (oldAvatar == null)
                _context.Avatars.Add(newAvatar);
            else
            {
                _context.Entry(oldAvatar).State = EntityState.Detached;
                _context.Avatars.Attach(newAvatar);
                _context.Entry(newAvatar).State = EntityState.Modified;
            }

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}

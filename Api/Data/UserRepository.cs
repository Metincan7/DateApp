using Api.Entites;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Api.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context=context;
        }
        public async Task<IEnumerable<AppUser>> GetUserAsync()
        {
            return await _context.AppUsers.ToListAsync();
        }

        public Task<AppUser> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.AppUsers.SingleOrDefaultAsync(x=>x.UserName==username);

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }


        public void Updata(AppUser user)
        {
            _context.Entry(user).State=EntityState.Modified; 
        }

    }
}
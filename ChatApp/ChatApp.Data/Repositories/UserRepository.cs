using ChatApp.Data.Interfaces;
using ChatApp.Data.Shared;
using ChatApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ChatDbContext _context;

    public UserRepository(ChatDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        if(dbUser != null)
        {
            user.Id = dbUser.Id;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        return null;
    }

    public async Task<OperationResult> DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return OperationResult.Failure;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return OperationResult.Success;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsByUserNameAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.UserName == username);
    }
}
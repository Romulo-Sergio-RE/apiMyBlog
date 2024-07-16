using api.Context;
using api.Dtos.User;
using api.Helpers;
using api.Interface;
using api.Models;
using api.utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> DeleteUserAsync(int id)
    {
        var userId = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (userId == null)
        {
            return null;
        }
        _context.Users.Remove(userId);
        await _context.SaveChangesAsync();
        return userId;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var user = await _context.Users.Include(a => a.Articles).Include(c => c.Comments).ToListAsync();

        return user;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var userId = await _context.Users.Include(a => a.Articles)
            .Include(c => c.Comments)
            .FirstOrDefaultAsync(u => u.Id == id);
        if (userId == null)
        {
            return null;
        }
        return userId;
    }

    public async Task<User?> UpdateUserAsync(int id, UpdateUserRequestDto updateUser, string imageName)
    {
        var cripto = new UserPasswordCriptoService();
        var passwordCripto = cripto.ReturnMD5(updateUser.Password);

        var userModel = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (userModel == null)
        {
            return null;
        }
        userModel.Name = updateUser.Name;
        userModel.Email = updateUser.Email;
        userModel.Password = passwordCripto;
        userModel.UserImageName = imageName;
        userModel.Genre = updateUser.Genre;
        userModel.Roles = updateUser.Roles;
        await _context.SaveChangesAsync();
        return userModel;
    }
    public async Task<bool> UserIsAdmin(int id)
    {
        var userAdmin = await GetUserByIdAsync(id);

        if (await _context.Users.AnyAsync(u => u.Id == id) && userAdmin?.Roles == "admin")
        {
            return true;
        }
        return false;
    }
    public async Task<bool> UserExist(int id)
    {
        var userAdmin = await GetUserByIdAsync(id);
        if (await _context.Users.AnyAsync(u => u.Id == id))
        {
            return true;
        }
        return false;
    }
}

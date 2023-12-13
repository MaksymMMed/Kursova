using Microsoft.EntityFrameworkCore;
using SpeechBackend.DAL.Entity;
using SpeechBackend.DAL.Repos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBackend.DAL.Repos.Realization
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async override Task<User> GetCompleteById(int id)
        {
            User? user = await table.Where(x => x.Id == id).Include(x => x.Attemps).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public async Task<User> GetUserByEmail(string email, string password)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password!));
            string hashString = BitConverter.ToString(hashBytes).Replace("-", "");

            var user = await table.Where(x => x.Email!.Trim() == email!.Trim() && x.Password!.Trim() == hashString!.Trim())
                .Include(x => x.Attemps!)
                .FirstOrDefaultAsync();


            if (user == null)
            {
                throw new ArgumentException("Wrong email or password");
            }
            else
            {
                return user;
            }
        }

        public override async Task Insert(User user)
        {
            try
            {
                var entity = await table.FirstOrDefaultAsync(x => x.Email == user.Email);
                if (entity == null)
                {
                    user.Role = "user";

                    using var sha256 = SHA256.Create();
                    var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(user.Password!));
                    string hashString = BitConverter.ToString(hashBytes).Replace("-", "");

                    user.Password = hashString;

                    await table.AddAsync(user);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("User with this email already exists");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

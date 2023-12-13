using AutoMapper;

using SpeechBackend.BLL.DTO.Request;
using SpeechBackend.BLL.DTO.Response;
using SpeechBackend.BLL.Services.Interfaces;
using SpeechBackend.DAL.Entity;
using SpeechBackend.DAL.Repos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBackend.BLL.Services.Realizations
{
    public class UserService : IUserService
    {
        readonly IUserRepository repository;
        readonly IMapper mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task AddUser(AddUserRequest request)
        {
            var item = mapper.Map<User>(request);

            await repository.Insert(item);
        }

        public async Task DeleteUser(int id)
        {
            await repository.Delete(id);
        }

        public async Task<GetUserResponse> GetUserById(int id)
        {
            var item = await repository.GetById(id);
            return mapper.Map<GetUserResponse>(item);
        }

        public async Task UpdateUser(AddUserRequest request)
        {
            var item = mapper.Map<User>(request);
            await repository.Update(item);
        }

        public async Task<GetUserResponse> GetUserByEmail(string email, string password)
        {
            var item = await repository.GetUserByEmail(email,password);
            return mapper.Map<GetUserResponse>(item);
        }
    }
}

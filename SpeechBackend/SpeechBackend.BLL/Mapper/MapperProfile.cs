using AutoMapper;
using SpeechBackend.BLL.DTO.Request;
using SpeechBackend.BLL.DTO.Response;
using SpeechBackend.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBackend.BLL.Mapper
{
    public class MapperProfile:Profile
    {
        void AttempProfile()
        {
            CreateMap<Attemp, GetAttempResponse>();
            CreateMap<AddAttempRequest, Attemp>();
        }
        void UserProfile()
        {
            CreateMap<User, GetUserResponse>();
            CreateMap<AddUserRequest, User>();
        }
        public MapperProfile()
        {
            AttempProfile();
            UserProfile();
        }
    }
}

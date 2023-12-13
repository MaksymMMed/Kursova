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

namespace SpeechBackend.BLL.Services.Realization
{
    public class AttempService : IAttempService
    {
        readonly IAttempRepository repository;
        readonly IMapper mapper;

        public AttempService(IAttempRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task AddAttemp(AddAttempRequest request)
        {
            var item = mapper.Map<Attemp>(request);
            await repository.Insert(item);
        }

        public async Task<GetAttempResponse> GetAttemp(int id)
        {
            var item = await repository.GetById(id);
            return mapper.Map<GetAttempResponse>(item);
        }

        public async Task<List<Attemp>> GetUserAttemps(int id)
        {
            var item = await repository.GetUserSentences(id);
            return item;

        }
    }
}

using AutoMapper;
using InterviewsApp.Core.DTOs.External;
using InterviewsApp.Core.Interfaces;
using InterviewsApp.Core.Models;
using InterviewsApp.Data.Abstractions.Interfaces;
using InterviewsApp.Data.Models.Entities;
using InterviewsApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.Services
{
    public class LocalizationService : BaseDbService<LocalizationEntity, LocalizationDto>, ILocalizationService
    {

        private readonly IRepository<UserEntity> _userRepository;
        public LocalizationService(IRepository<LocalizationEntity> repository, IRepository<UserEntity> userRepository, IMapper mapper) : base(repository, mapper)
        {
            _userRepository = userRepository;
        }
        public async Task<Response<IEnumerable<LocalizationDto>>> GetByLanguage(string language)
        { 
            var localsByLanguage =  await _repository.Get(loc => loc.Language == language);
            var localsDtos = localsByLanguage.Select(e => _mapper.Map<LocalizationDto>(e));
            return new Response<IEnumerable<LocalizationDto>>(localsDtos);
        }
        public async Task<Response<IEnumerable<LocalizationDto>>> GetByUserId(Guid userId)
        {
            var user = (await _userRepository.GetByIdOrDefault(userId));
            if (user == null) 
            { 
                return new Response<IEnumerable<LocalizationDto>>("Loc.Message.NoSuchUser"); 
            }

            return await GetByLanguage(user.Language ?? "EN");
        }

        public async Task AddLocalization(LocalizationDto localizationDto)
        {
            await _repository.Create(_mapper.Map<LocalizationEntity>(localizationDto));
        }
        public async Task<Response> SetLocalizationForUser(Guid userId, string langCode)
        {
            var user = await _userRepository.GetByIdOrDefault(userId);
            if (user != null)
            {
                user.Language = langCode;
                await _userRepository.Update(user);
                return new Response();
            }
            return new Response("Loc.Message.NoSuchUser");
        }
    }
}

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
    internal class LocalizationService : BaseDbService<LocalizationEntity, LocalizationDto>, ILocalizationService
    {

        private readonly IRepository<UserEntity> _userRepository;
        public LocalizationService(IRepository<LocalizationEntity> repository, IRepository<UserEntity> userRepository, IMapper mapper) : base(repository, mapper)
        {
            _userRepository = userRepository;
        }
        public async Task<Response<IEnumerable<LocalizationDto>>> GetByLanguage(string language)
        { 
            var localsByLanguage =  await _repository.Get(loc => loc.Language == language);
            return new Response<IEnumerable<LocalizationDto>>(_mapper.Map<IEnumerable<LocalizationDto>>(localsByLanguage));
        }
        public async Task<Response<IEnumerable<LocalizationDto>>> GetByUserId(Guid userId)
        {
            var lang = (await _userRepository.GetByIdOrDefault(userId))?.Language;
            if (lang == null) 
            { 
                return new Response<IEnumerable<LocalizationDto>>("Данного пользователя не существует"); 
            }

            return await GetByLanguage(lang);
        }

        public async Task AddLocalization(LocalizationDto localizationDto)
        {
            await _repository.Create(_mapper.Map<LocalizationEntity>(localizationDto));
        }
    }
}

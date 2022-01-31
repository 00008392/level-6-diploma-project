using AutoMapper;
using BaseClasses.Contracts;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Services
{
    public class InfoService : IInfoService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Category> _categoryRepository;

        public InfoService(
            IMapper mapper,
            IRepository<City> cityRepository,
            IRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ICollection<CategoryCityDTO>> GetAllCategoriesAsync()
        {
            var categories = (await _categoryRepository.GetAllAsync()).ToList();
            return _mapper.Map<ICollection<Category>, ICollection<CategoryCityDTO>>(categories);
        }

        public async Task<ICollection<CategoryCityDTO>> GetAllCitiesAsync()
        {
            var cities = (await _cityRepository.GetAllAsync()).ToList();
            return _mapper.Map<ICollection<City>, ICollection<CategoryCityDTO>>(cities);
        }
    }
}

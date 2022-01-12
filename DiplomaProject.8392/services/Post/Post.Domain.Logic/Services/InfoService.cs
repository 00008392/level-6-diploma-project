using AutoMapper;
using BaseClasses.Contracts;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Services
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

        public async Task<ICollection<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = (await _categoryRepository.GetAllAsync()).ToList();
            return _mapper.Map<ICollection<Category>, ICollection<CategoryDTO>>(categories);
        }

        public async Task<ICollection<CityDTO>> GetAllCitiesAsync()
        {
            var cities = (await _cityRepository.GetAllAsync()).ToList();
            return _mapper.Map<ICollection<City>, ICollection<CityDTO>>(cities);
        }
    }
}

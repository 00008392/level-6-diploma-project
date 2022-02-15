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
    //service for retrieving entities related to post and necessary for post creation and modification
    //in this case, such entities are city, category, rule, facility
    public class PostRelatedInfoService<T> : IPostRelatedInfoService<T> where T: Item
    {
        private readonly IMapper _mapper;
        private readonly IRepository<T> _repository;

        public PostRelatedInfoService(
            IMapper mapper,
            IRepository<T> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ICollection<ItemDTO>> GetAllItemsAsync()
        {
            //get all entities from database and map them to dtos
            var items = (await _repository.GetAllAsync()).ToList();
            return _mapper.Map<ICollection<T>, ICollection<ItemDTO>>(items);
        }

    }
}

using AutoMapper;
using ExceptionHandling;
using FluentValidation;
using Post.Domain.Core;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.DTOs.Core;
using Protos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API.Services.Strategies
{
    public class FeedbackStrategy<T, E> : IFeedbackStrategy<T, E> where T : FeedbackEntity
                                                                  where E: IFeedbackEntityDTO
    {
        private readonly IMapper _mapper;
        private readonly IFeedbackService<T, E> _service;

        public FeedbackStrategy(IMapper mapper, IFeedbackService<T, E> service)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task<Response> DeleteFeedbackAsync(Request request)
        {
            var response = new Response();
            try
            {
                await _service.DeleteFeedbackAsync(request.Id);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }

        public async Task<FeedbackInfoResponse> GetFeedbackDetailsAsync(Request request)
        {
            var feedback = await _service.GetFeedbackDetailsAsync(request.Id);

            if (feedback == null)
            {
                return new FeedbackInfoResponse
                {
                    NoFeedback = true
                };
            }
            var response = _mapper.Map<FeedbackInfoResponse>(feedback);
            return response;
        }

        public async Task<FeedbacksListResponse> GetFeedbacksAsync(Request request)
        {
            var reply = new FeedbacksListResponse();
            var feedbacksDTO = await _service.GetFeedbacksAsync(request.Id);
            if (feedbacksDTO != null)
            {
                var feedbacks = _mapper.Map<ICollection<FeedbackInfoResponse>>(feedbacksDTO);
                reply.Feedbacks.AddRange(feedbacks);
            }
            return reply;
        }

        public async Task<Response> LeaveFeedbackAsync(CreateFeedbackRequest request)
        {
            if (request == null)
            {
                return new Response
                {
                    Message = "Empty request"
                };
            }
            var createFeedbackDTO = _mapper.Map<FeedbackDTO>(request);

            var response = new Response();
            try
            {
                await _service.LeaveFeedbackAsync(createFeedbackDTO);
                response.IsSuccess = true;

            }
            catch (ValidationException ex)
            {
                response.HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                response.HandleException(ex);
            }
            return response;
        }
    }
}

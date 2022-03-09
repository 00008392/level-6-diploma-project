using FrontEndApp.Models;
using FrontEndApp.Models.Core;
using FrontEndApp.Models.Feedback;
using FrontEndApp.Services.Core;
using FrontEndApp.Services.Feedback.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Feedback
{
    //base service that consumes feedback api
    public abstract class FeedbackServiceBase<T> : BaseService, IFeedbackService<T> where T : IFeedbackItem
    {
        protected readonly string _url;
        protected FeedbackServiceBase(
            HttpClient client,
            string url)
            :base(client)
        {
            _url = url;
        }
        //check if current user can leave feedback on item
        public async Task<bool> CanLeaveFeedbackAsync(long itemId)
        {
            bool canLeaveFeedback;
            try
            {
                //call api
                var reply = await _client.GetAsync($"{_url}/can-leave-feedback-on/{itemId}");
                if (reply.IsSuccessStatusCode)
                {
                    //in case of success, parse response as string and convert to boolean
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    canLeaveFeedback = bool.Parse(responseStr);
                }
                else
                {
                    //in case of error response, set to false
                    canLeaveFeedback = false;
                }
            }
            catch
            {
                //in case of exception, set to false
                canLeaveFeedback = false;
            }
            return canLeaveFeedback;
        }
        //delete feedback
        public async Task<Response> DeleteFeedbackAsync(long id,
            Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //call base service method for delete
            return await HandleDeleteActionAsync($"{_url}/{id}", onSuccessAction, onErrorAction);
        }
        //get average rating for item
        public async Task<double?> GetAverageRatingAsync(long itemId)
        {
            try
            {
                //call api
                var reply = await _client.GetAsync($"{_url}/rating/{itemId}");
                var avgRatingResponse = new AverageRatingResponse();
                if (reply.IsSuccessStatusCode)
                {
                    //in case of success, parse response 
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    avgRatingResponse = JsonConvert.DeserializeObject<AverageRatingResponse>
                        (responseStr);
                    if(avgRatingResponse?.NoRating!=true)
                    {
                        //if there is rating, round it to 1 decimal place
                        return Math.Round((double)avgRatingResponse.Rating, 1);
                    }
                }
            }
            catch
            {
            }
            //in case of error response or exception, return null
            return null;
        }
        //get feedbacks for item
        public async Task<ICollection<FeedbackResponse>> GetFeedbacksForItemAsync(long id)
        {
            //call base service method for multiple items retrieval
            return await HandleMultipleItemsRetrievalAsync<FeedbackResponse>($"{_url}/item/{id}");
        }
        //create new feedback
        public async Task<Response> LeaveFeedbackAsync(CreateFeedback feedback,
            Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //call base service method for create
            return await HandleCreateActionAsync(feedback, _url, onSuccessAction, onErrorAction);
        }
    }
}

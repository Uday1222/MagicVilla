﻿using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse APIResponse { get; set; }

        public IHttpClientFactory httpClient;

        public BaseService(IHttpClientFactory httpClient)
        {
            APIResponse = new();
            this.httpClient = httpClient;
        }
        public async Task<T> SendAsync<T>(APIRequest request)
        {
            try
            {

                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(request.Url);

                if (request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
                }

                switch (request.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }

                HttpResponseMessage apiResponse = null;

                if (!string.IsNullOrEmpty(request.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);
                }

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                try
                {
                    var APIResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);

                    if (apiResponse != null && APIResponse != null)
                    {
                        APIResponse.StatusCode = apiResponse.StatusCode;
                        APIResponse.IsSuccess = apiResponse.IsSuccessStatusCode;

                        if (APIResponse.StatusCode == System.Net.HttpStatusCode.BadRequest ||
                        APIResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            APIResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                            APIResponse.IsSuccess = false;
                            var res = JsonConvert.SerializeObject(APIResponse);
                            var response = JsonConvert.DeserializeObject<T>(res);
                            return response;
                        }
                    }

                }
                catch (Exception ex)
                {
                    var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return APIResponse;
                }
                var response1 = JsonConvert.DeserializeObject<T>(apiContent);
                return response1;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { ex.Message },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }

        }
    }
}

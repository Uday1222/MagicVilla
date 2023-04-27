﻿using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using static MagicVilla_Utility.SD;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private IHttpClientFactory _httpClient;

        private string villaUrl;

        public VillaService(IHttpClientFactory httpClient, IConfiguration configuration) :base(httpClient)
        {
            _httpClient = httpClient;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(CreateVillaDTO dto)
        {

            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = villaUrl + "api/VillaAPI",
                Data = dto
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.DELETE,
                Url = villaUrl + "api/VillaAPI/" + id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = villaUrl + "api/VillaAPI",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Url = villaUrl + "api/VillaAPI/" + id,
            });
        }

        public Task<T> UpdateAsync<T>(UpdateVillaDTO dto)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.PUT,
                Url = villaUrl + "api/VillaAPI/" + dto.Id,
                Data = dto
            });
        }
    }
}

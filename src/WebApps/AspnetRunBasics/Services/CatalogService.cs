﻿using AspnetRunBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client;
        }

        public Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            throw new NotImplementedException();
        }

        public Task<CatalogModel> GetCatalog(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            throw new NotImplementedException();
        }
    }
}

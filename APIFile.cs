using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class NeiHttpClient
    {
        private readonly HttpClient neihttpClient;
        private readonly string baseUrl;

        public NeiHttpClient(string baseUrl)
        {
           neihttpClient = new HttpClient();
           this.baseUrl = baseUrl;
        }

        public async Task<ModelApi<string>> GetAsync()
        {
            try
            {
                HttpResponseMessage response = await neihttpClient.GetAsync(baseUrl);
                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadAsStringAsync();

                return new ModelApi<string>
                {
                    Message = "Data retrieved successfully",
                    StatusCode = response.StatusCode,
                    Information = new List<string> { data }
                };
            }
            catch (HttpRequestException ex)
            {
                return new ModelApi<string>
                {
                    Message = $"Error occurred: {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<ModelApi<string>> PostAsync(Dictionary<string, string> parameters)
        {
            try
            {
                var content = new FormUrlEncodedContent(parameters);
                HttpResponseMessage response = await neihttpClient.PostAsync(baseUrl, content);
                response.EnsureSuccessStatusCode();

                var responseData = await response.Content.ReadAsStringAsync();

                return new ModelApi<string>
                {
                    Message = "Data posted successfully",
                    StatusCode = response.StatusCode,
                    Information = new List<string> { responseData }
                };
            }
            catch (HttpRequestException ex)
            {
                return new ModelApi<string>
                {
                    Message = $"Error occurred: {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
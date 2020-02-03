using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TheMonitaur.Lib.DTOs;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.WebAPI
{
    public class WebAPIClient : IWebAPIClient
    {
        protected readonly string _webAPIBaseUri;
        protected string _accessToken;

        public WebAPIClient(string accessToken, string webAPIBaseUri = "https://api.themonitaur.com")
        {
            _accessToken = accessToken;
            _webAPIBaseUri = webAPIBaseUri;
        }

        /// <summary>
        /// Set the access token
        /// </summary>
        /// <param name="accessToken">The access token to be used when requesting The Monitaur's WebAPI</param>
        public virtual void SetAccessToken(string accessToken)
        {
            _accessToken = accessToken;
        }

        /// <summary>
        /// Request the Authorized Client Application
        /// </summary>
        /// <returns>A Client Application data-transfer object</returns>
        public virtual async Task<ClientApplicationDTO> GetClientApplicationAsync()
        {
            return await GetAsync<ClientApplicationDTO>("clientApplication");
        }
        /// <summary>
        /// Read the Alerts for a Client Application
        /// </summary>
        /// <param name="clientApplicationId">The Client Application to retrieve the non-dismissed Alerts</param>
        /// <returns>An array of Alert data-transfer objects</returns>
        public virtual async Task<AlertDTO[]> GetAlertsAsync()
        {
            return await GetAsync<AlertDTO[]>("alerts");
        }
        /// <summary>
        /// Request an Alert
        /// </summary>
        /// <param name="id">The Id of the requested Alert</param>
        /// <returns>An Alert data-transfer object</returns>
        public virtual async Task<AlertDTO> GetAlertAsync(long id)
        {
            return await GetAsync<AlertDTO>("alert", id.ToString());
        }
        /// <summary>
        /// Create a new Alert
        /// </summary>
        /// <param name="request">An Alert create request</param>
        /// <returns>An Alert data-transfer object</returns>
        public virtual async Task<AlertDTO> CreateAlertAsync(AlertCreateRequest request)
        {
            return await PostAsync<AlertCreateRequest, AlertDTO>("alerts", request);
        }
        /// <summary>
        /// Dismiss up to 150 Alerts
        /// </summary>
        /// <param name="request">The Ids (up to 150) of the Alerts to dismiss</param>
        /// <returns>True if the Alerts were dismissed, and false if the Alerts could not be dismissed</returns>
        public virtual async Task<bool> DismissAlertsAsync(long[] ids)
        {
            return await PostAsync<AlertsDismissRequest, bool>("alerts/dismiss", new AlertsDismissRequest
            {
                Ids = ids
            });
        }
        /// <summary>
        /// Delete an existing Alert
        /// </summary>
        /// <param name="id">The Id of the requested Alert to delete</param>
        /// <returns>True if the delete was successful</returns>
        public virtual async Task<bool> DeleteAlertAsync(long id)
        {
            return await DeleteAsync("alerts", id);
        }

        /// <summary>
        /// The Dispose method for this class
        /// </summary>
        public virtual void Dispose()
        { }

        protected virtual void CheckIfTokenIsValid()
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
            {
                throw new Exception("There is no access token currently loaded to access the WebAPI. " +
                    "Please use SetAccesssToken to load a new access token and try again");
            }
        }
        protected virtual HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            return client;
        }
        protected virtual async Task<T> GetAsync<T>(string path, string parameters = "")
        {
            CheckIfTokenIsValid();

            try
            {
                using var client = CreateClient();
                var fullPath = $"{_webAPIBaseUri}/{path}" + (!string.IsNullOrWhiteSpace(parameters) ? $"/{parameters}" : string.Empty);
                var response = await client.GetAsync(fullPath);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
            }
            catch
            { }

            return default;
        }
        protected virtual async Task<U> PostAsync<T, U>(string path, T request)
        {
            CheckIfTokenIsValid();

            try
            {
                using var client = CreateClient();
                var fullPath = $"{_webAPIBaseUri}/{path}";
                var response = await client.PostAsync(fullPath, new JsonContent(request));

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
                }
            }
            catch
            { }

            return default;
        }
        protected virtual async Task<bool> DeleteAsync(string path, long id)
        {
            CheckIfTokenIsValid();

            try
            {
                using var client = CreateClient();
                var fullPath = $"{_webAPIBaseUri}/{path}/{id}";
                var response = await client.DeleteAsync(fullPath);
                return response.StatusCode == HttpStatusCode.NoContent;
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// A class containing the JSON payload
        /// </summary>
        public class JsonContent : StringContent
        {
            public JsonContent(object obj)
                : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
            {
            }
        }
    }
}

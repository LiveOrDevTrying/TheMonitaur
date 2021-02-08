using Newtonsoft.Json;
using System;
using System.Linq;
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
        protected string _token;
        private static HttpClient _client;
        private static object _clientLock = new object();

        public WebAPIClient(string token, string webAPIBaseUri = "https://api.themonitaur.com", HttpClient client = null)
        {
            _token = token;
            _webAPIBaseUri = webAPIBaseUri;

            if (client != null)
            {
                _client = client;
            }
            else
            {
                lock(_clientLock)
                {
                    if (_client == null)
                    {
                        _client = new HttpClient();
                    }
                }
            }
        }

        /// <summary>
        /// Set the token
        /// </summary>
        /// <param name="token">The token to be used when requesting The Monitaur's WebAPI</param>
        public virtual void SetToken(string token)
        {
            _token = token;
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
        /// Read the undismissed Alerts for a Client Application
        /// </summary>
        /// <param name="clientApplicationId">The Client Application to retrieve the non-dismissed Alerts</param>
        /// <returns>An array of Alert data-transfer objects</returns>
        public virtual async Task<AlertDTO[]> GetAlertsAsync()
        {
            return await GetAsync<AlertDTO[]>("alerts");
        }
        /// <summary>
        /// Read the Alerts for a Client Application within the AlertsLookupRequest criteria
        /// </summary>
        /// <param name="AlertsLookupRequest">An Alerts Lookup Request object</param>
        /// <returns>An array of Alert data-transfer objects</returns>
        public virtual async Task<AlertDTO[]> GetAlertsAsync(AlertsLookupRequest request)
        {
            var queryString = new StringBuilder();
            request.AlertTypes.ToList().ForEach(s =>
            {
                queryString.Append($"alertTypes={s}&");
            });
            request.StatusTypes.ToList().ForEach(s =>
            {
                queryString.Append($"statusTypes={s}&");
            });
            if (request.StartDate.HasValue)
            {
                queryString.Append($"startDate={request.StartDate.Value}&");
            }
            if (request.EndDate.HasValue)
            {
                queryString.Append($"endDate={request.EndDate.Value}&");
            }
            if (request.MaxRecordsToRetrieve.HasValue)
            {
                queryString.Append($"maxRecordsToRetrieve={request.MaxRecordsToRetrieve.Value}&");
            }
            if (request.IncludeActiveAlerts.HasValue)
            {
                queryString.Append($"includeActiveAlerts={request.IncludeActiveAlerts.Value}&");
            }
            if (request.IncludeActiveAlerts.HasValue)
            {
                queryString.Append($"includeDismissedAlerts={request.IncludeDismissedAlerts.Value}&");
            }
            return await GetAsync<AlertDTO[]>($"alerts?{queryString.ToString().Substring(0, queryString.ToString().Length - 1)}");
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
            if (string.IsNullOrWhiteSpace(_token))
            {
                throw new Exception("There is no access token currently loaded to access the WebAPI. " +
                    "Please use SetAccesssToken to load a new access token and try again");
            }
        }
        protected virtual async Task<T> GetAsync<T>(string path, string parameters = "")
        {
            CheckIfTokenIsValid();

            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var fullPath = $"{_webAPIBaseUri}/{path}" + (!string.IsNullOrWhiteSpace(parameters) ? $"/{parameters}" : string.Empty);
                var response = await _client.GetAsync(fullPath);

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
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var fullPath = $"{_webAPIBaseUri}/{path}";
                var response = await _client.PostAsync(fullPath, new JsonContent(request));

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
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var fullPath = $"{_webAPIBaseUri}/{path}/{id}";
                var response = await _client.DeleteAsync(fullPath);
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

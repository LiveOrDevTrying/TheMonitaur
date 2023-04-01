using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheMonitaur.Lib.DTOs;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.WebAPI
{
    public class WebAPIClient : IWebAPIClient
    {
        protected readonly string _webAPIBaseUri;
        protected readonly HttpClient _httpClient;
        protected string _token;

        /// <summary>
        /// Constructor for WebAPIClient
        /// </summary>
        /// <param name="token">OAuth token for the application registered on The Monitaur</param>
        /// <param name="webAPIBaseUri">Optional - The API URI for The Monitaur</param>
        /// <param name="httpClientFactory">Optional - Http client injection for Dependency Injection</param>
        public WebAPIClient(string token, string webAPIBaseUri = "https://api.themonitaur.com", HttpClient httpClient = null)
        {
            _token = token;
            _webAPIBaseUri = webAPIBaseUri;

            if (httpClient != null)
            {
                _httpClient = httpClient;
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
        /// Get the Authorized Client Application
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>A Client Application data-transfer object</returns>
        public virtual async Task<ClientApplicationDTO> GetClientApplicationAsync(CancellationToken cancellationToken = default)
        {
            return await GetAsync<ClientApplicationDTO>("clientApplication", cancellationToken);
        }
        /// <summary>
        /// Gets all undismissed Alerts
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>An array of Alert data-transfer objects</returns>
        public virtual async Task<AlertDTO[]> GetAlertsAsync(CancellationToken cancellationToken = default)
        {
            return await GetAsync<AlertDTO[]>("alerts", cancellationToken);
        }
        /// <summary>
        /// Get the Alerts for a Client Application within the AlertsLookupRequest criteria
        /// </summary>
        /// <param name="request">An Alerts Lookup Request object</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>An array of Alert data-transfer objects</returns>
        public virtual async Task<AlertDTO[]> GetAlertsAsync(AlertsLookupRequest request, CancellationToken cancellationToken = default)
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
            return await GetAsync<AlertDTO[]>($"alerts?{queryString.ToString().Substring(0, queryString.ToString().Length - 1)}", cancellationToken);
        }
        /// <summary>
        /// Get an Alert
        /// </summary>
        /// <param name="id">The Id of the requested Alert</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>An Alert data-transfer object</returns>
        public virtual async Task<AlertDTO> GetAlertAsync(long id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<AlertDTO>("alert", cancellationToken, id.ToString());
        }
        /// <summary>
        /// Create a new Alert
        /// </summary>
        /// <param name="request">An Alert create request</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>An Alert data-transfer object</returns>
        public virtual async Task<AlertDTO> CreateAlertAsync(AlertCreateRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Message.Trim().Length > 255)
            {
                throw new Exception("Max message length is 255 characters");
            }

            return await PostAsync<AlertCreateRequest, AlertDTO>("alerts", request, cancellationToken);
        }
        /// <summary>
        /// Dismiss up to 150 Alerts
        /// </summary>
        /// <param name="ids">The Ids (up to 150) of the Alerts to dismiss</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>True if the Alerts were dismissed, and false if the Alerts could not be dismissed</returns>
        public virtual async Task<bool> DismissAlertsAsync(long[] ids, CancellationToken cancellationToken = default)
        {
            return await PostAsync<AlertsSelectedRequest, bool>("alerts/dismiss", new AlertsSelectedRequest
            {
                Ids = ids
            }, cancellationToken);
        }
        /// <summary>
        /// Delete an existing Alert
        /// </summary>
        /// <param name="id">The Id of the requested Alert to delete</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>True if the delete was successful</returns>
        public virtual async Task<bool> DeleteAlertAsync(long id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync("alerts", id, cancellationToken);
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
        protected virtual async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken, string parameters = "")
        {
            CheckIfTokenIsValid();

            var client = _httpClient;

            if (client == null)
            {
                client = new HttpClient();
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var fullPath = $"{_webAPIBaseUri}/{path}" + (!string.IsNullOrWhiteSpace(parameters) ? $"/{parameters}" : string.Empty);

            var response = await client.GetAsync(fullPath, cancellationToken);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }

            return default;
        }
        protected virtual async Task<U> PostAsync<T, U>(string path, T request, CancellationToken cancellationToken)
        {
            CheckIfTokenIsValid();

            var client = _httpClient;

            if (client == null)
            {
                client = new HttpClient();
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var fullPath = $"{_webAPIBaseUri}/{path}";

            var response = await client.PostAsync(fullPath, new JsonContent(request), cancellationToken);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
            }

            return default;
        }
        protected virtual async Task<bool> DeleteAsync(string path, long id, CancellationToken cancellationToken)
        {
            CheckIfTokenIsValid();

            var client = _httpClient;

            if (client == null)
            {
                client = new HttpClient();
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var fullPath = $"{_webAPIBaseUri}/{path}/{id}";

            var response = await client.DeleteAsync(fullPath, cancellationToken);
            return response.StatusCode == HttpStatusCode.NoContent;
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

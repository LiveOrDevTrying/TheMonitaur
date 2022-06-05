using System;
using System.Threading;
using System.Threading.Tasks;
using TheMonitaur.Lib.DTOs;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.WebAPI
{
    /// <summary>
    /// The Interface for The Monitaur's .NET WebAPI Client Services
    /// </summary>
    public interface IWebAPIClient : IDisposable
    {
        /// <summary>
        /// Get the Authorized Client Application
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>A Client Application data-transfer object</returns>
        Task<ClientApplicationDTO> GetClientApplicationAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets all undismissed Alerts
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>An array of Alert data-transfer objects</returns>
        Task<AlertDTO[]> GetAlertsAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Get the Alerts for a Client Application within the AlertsLookupRequest criteria
        /// </summary>
        /// <param name="request">The Alerts lookup request</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>An array of Alert data-transfer objects</returns>
        Task<AlertDTO[]> GetAlertsAsync(AlertsLookupRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Get an Alert
        /// </summary>
        /// <param name="id">The Id of the Alert to retrieve</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns></returns>
        Task<AlertDTO> GetAlertAsync(long id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Create a new Alert
        /// </summary>
        /// <param name="request">The Alert create request</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>An Alert data-transfer object</returns>
        Task<AlertDTO> CreateAlertAsync(AlertCreateRequest request, CancellationToken cancellationToken = default);
        /// <summary>
        /// Dismiss up to 150 Alerts
        /// </summary>
        /// <param name="ids">An array of the Ids of the requested Alerts to dismiss</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>True if the Alerts were dismissed successfully, and false if the Alerts could not be dismissed</returns>
        Task<bool> DismissAlertsAsync(long[] ids, CancellationToken cancellationToken = default);
        /// <summary>
        /// Delete an Alert
        /// </summary>
        /// <param name="id">The Id of the Alert to delete</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>True if the Alerts was deleted successfully, and false if the Alert could not be deletewd</returns>
        Task<bool> DeleteAlertAsync(long id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Set the Token to a new Token
        /// </summary>
        /// <param name="accessToken">The new Access Token to use for WebAPI requests</param>
        void SetToken(string accessToken);
    }
}
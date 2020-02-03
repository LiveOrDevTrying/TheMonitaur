using System;
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
        /// <returns>A Client Application data-transfer object</returns>
        Task<ClientApplicationDTO> GetClientApplicationAsync();
        /// <summary>
        /// Gets the Alerts for the Authorized Client Application
        /// </summary>
        /// <returns>An array of Alert data-transfer objects</returns>
        Task<AlertDTO[]> GetAlertsAsync();
        /// <summary>
        /// Get an Alert for the Authorized Client Application
        /// </summary>
        /// <param name="id">The Id of the Alert to retrieve</param>
        /// <returns></returns>
        Task<AlertDTO> GetAlertAsync(long id);
        /// <summary>
        /// Create a new Alert
        /// </summary>
        /// <param name="request">The Alert create request</param>
        /// <returns>An Alert data-transfer object</returns>
        Task<AlertDTO> CreateAlertAsync(AlertCreateRequest request);
        /// <summary>
        /// Dismiss the Alerts requested by their Id
        /// </summary>
        /// <param name="ids">An array of the Ids of the requested Alerts to dismiss</param>
        /// <returns>True if the Alerts were dismissed successfully, and false if the Alerts could not be dismissed</returns>
        Task<bool> DismissAlertsAsync(long[] ids);
        /// <summary>
        /// Delete an Alert
        /// </summary>
        /// <param name="id">The Id of the Alert to delete</param>
        /// <returns>True if the Alerts was deleted successfully, and false if the Alert could not be deletewd</returns>
        Task<bool> DeleteAlertAsync(long id);
        /// <summary>
        /// Set the Access Token to a new Token
        /// </summary>
        /// <param name="accessToken">The new Access Token to use for WebAPI requests</param>
        void SetAccessToken(string accessToken);
    }
}
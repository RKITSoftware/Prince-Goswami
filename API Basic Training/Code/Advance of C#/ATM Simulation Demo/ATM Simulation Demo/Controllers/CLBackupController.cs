using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATM_Simulation_Demo.Others.Auth;

namespace ATM_Simulation_Demo.Controllers
{
    /// <summary>
    /// Controller for managing backup operations.
    /// </summary>
    [CustomAuthenticationFilter]
    public class BackupController : ApiController
    {
        #region Fields

        // Service instance for backup operations
        private readonly BackupService _backupService = new BackupService();

        #endregion

        /// <summary>
        /// Retrieves a backup of data.
        /// </summary>
        /// <remarks>
        /// Requires authentication with role "A".
        /// </remarks>
        /// <returns>HTTP response containing the backup data.</returns>
        [HttpGet]
        [Route("api/backup")]
        [CustomAuthorizationFilter(Roles = "Admin")]
        public HttpResponseMessage BackupData()
        {
            try
            {
                // Perform data backup
                string backupFilePath = _backupService.BackupData();

                if (backupFilePath != null)
                {
                    // If backup successful, return backup file
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StreamContent(File.OpenRead(backupFilePath));
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = "backup.json";
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    return response;
                }
                else
                {
                    // If backup failed, return internal server error
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Backup failed");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception and return internal server error
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Backup failed");
            }
        }
    }
}

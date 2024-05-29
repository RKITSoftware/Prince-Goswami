using ATM_Simulation_Demo.BAL.Interface;
using ATM_Simulation_Demo.BAL.Services;
using ATM_Simulation_Demo.DAL;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ATM_Simulation_Demo.Controllers
{

    public class BackupController : ApiController
    {
        #region fields

        private readonly BackupService _backupService = new BackupService();

        private readonly static IBLPinModule _pinModule = new PinModule();

        private readonly static IBLAccountRepository _accountRepo = new AccountRepository(_pinModule);
        private readonly IBLAccountService _accountService = new AccountService(_accountRepo, _pinModule);

        private readonly static IBLTransactionRepository _transactionRepo = new TransactionRepository();
        private readonly IBLTransactionService _transactionService = new TransactionService(_accountRepo, _transactionRepo);

        private readonly IBLLimitService _limitService = new LimitService();

        private readonly static IBLUserRepository _userRepo = new UserRepository();
        private readonly IBLUserService _userService = new UserService(_userRepo);

        #endregion

        [HttpGet]
        [Route("api/backup")]
        public HttpResponseMessage BackupData()
        {
            try
            {
                string backupFilePath = _backupService.BackupData();

                if (backupFilePath != null)
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StreamContent(File.OpenRead(backupFilePath));
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = "backup.json";
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Backup failed");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Backup failed");
            }
        }
    }

}

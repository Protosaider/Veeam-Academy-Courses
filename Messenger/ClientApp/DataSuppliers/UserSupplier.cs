using System;
using System.Windows;
using ClientApp.DataSuppliers.Data;
using ClientApp.Other;
using ClientApp.ServiceProxies;
using DTO;
using log4net;

namespace ClientApp.DataSuppliers
{
	internal sealed class CUserSupplier : IUserSupplier, IDisposable
    {
        private readonly CUserServiceProxy _service;
        private readonly ILog _logger = SLogger.GetLogger();

        private CUserSupplier()
        {
            _service = new CUserServiceProxy();
        }

        public static CUserSupplier Create()
        {
            try
            {
                return new CUserSupplier();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        #region IDisposable

        public void Dispose() => _service.Dispose();

        #endregion

        #region IUserSupplier

        public Boolean UpdateActivityStatus(Guid userId, Int32 currentStatus)
        {
            _logger.LogInfo($"Supplier method '{nameof(UpdateActivityStatus)}({userId}, {currentStatus})' is called");
            CActivityStatusDto activityStatus = new CActivityStatusDto(userId, currentStatus); 
            return _service.UpdateActivityStatus(activityStatus);
        }

        public Boolean UpdateLastActiveDate(Guid userId, DateTimeOffset currentDate)
        {
            _logger.LogInfo($"Supplier method '{nameof(UpdateLastActiveDate)}({userId}, {currentDate})' is called");

            CLastActiveDateDto lastActive = new CLastActiveDateDto(userId, currentDate);

            return _service.UpdateLastActiveDate(lastActive);
        }

        public CUserData GetUserData(Guid userId)
        {
            _logger.LogInfo($"Supplier method '{nameof(GetUserData)}({userId})' is called");

            var userDto = _service.GetUserData(userId);
            return userDto != null ? new CUserData(userDto.Login) : null;
        }

        #endregion

    }
}


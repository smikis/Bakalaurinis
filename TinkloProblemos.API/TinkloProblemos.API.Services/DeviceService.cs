using System;
using System.Collections.Generic;
using System.Text;
using TinkloProblemos.API.Contracts.Devices;
using TinkloProblemos.API.Interfaces.Repositories;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public bool Add(DeviceDto device)
        {
            if (_deviceRepository.Add(device) != 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<DeviceDto> GetAll()
        {
            return _deviceRepository.GetAll();
        }

        public IEnumerable<DeviceDto> GetAllInternetUserDevices(int internetUserId)
        {
            return _deviceRepository.GetAll(internetUserId);
        }

        public bool Update(DeviceDto device, int deviceId)
        {
            if (_deviceRepository.Update(device, deviceId) != 0)
            {
                return true;
            }
            return false;
        }
    }
}

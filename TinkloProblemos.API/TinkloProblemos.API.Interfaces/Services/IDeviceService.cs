using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Devices;

namespace TinkloProblemos.API.Interfaces.Services
{
    public interface IDeviceService
    {
        bool Add(DeviceDto device);
        IEnumerable<DeviceDto> GetAll();
        IEnumerable<DeviceDto> GetAllInternetUserDevices(int internetUserId);
        bool Update(DeviceDto device, int deviceId);
    }
}
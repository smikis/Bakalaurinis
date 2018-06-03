using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Devices;

namespace TinkloProblemos.API.Interfaces.Repositories
{
    public interface IDeviceRepository
    {
        int Add(DeviceDto prod);
        IEnumerable<DeviceDto> GetAll();
        IEnumerable<DeviceDto> GetAll(int internetUserId);
        int Update(DeviceDto prod, int internetUserId);
    }
}
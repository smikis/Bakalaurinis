using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Database.Queries
{
    public static class DeviceQueries
    {
        public static string Add = @"INSERT INTO internetuserdevice (Name, Manufacturer, MacAddress, WarrantyExpiration,Description,InternetUserId) 
                                    VALUES(@Name, @Manufacturer, @MacAddress, @WarrantyExpiration, @Description, @InternetUserId)";

        public static string GetAll = @"SELECT * FROM internetuserdevice;";

        public static string GetInternetUserDevices = @"SELECT * FROM internetuserdevice
                                                         WHERE internetuserdevice.internetUserId = @internetUserId";

        public static string Update = @"UPDATE internetuserdevice
                                        SET Name = @Name,
                                        Manufacturer =  @Manufacturer,
                                        MacAddress =  @MacAddress,
                                        WarrantyExpiration =  @WarrantyExpiration,
                                        Description =  @Description,
                                        InternetUserId =  @InternetUserId
                                        WHERE Id = @deviceId";

    }
}

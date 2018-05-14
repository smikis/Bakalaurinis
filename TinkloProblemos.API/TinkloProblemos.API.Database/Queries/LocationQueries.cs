using System;
using System.Collections.Generic;
using System.Text;

namespace TinkloProblemos.API.Database.Queries
{
    public static class LocationQueries
    {
        public static string InsertUserCoordinates = @"
INSERT INTO current_user_coordinates(user_id,lat,lng,modifyDate)
VALUES(@userId,@Lat,@Lng,NOW());";

        public static string UpdateUserCoordinates = @"
UPDATE current_user_coordinates
SET lat = @Lat, Lng = @lng, modifyDate = NOW()
WHERE user_id = @userId;";

        public static string GetCoordinates = @"
SELECT * FROM bakalaurinis.current_user_coordinates
WHERE user_id = @userId;";
    }
}

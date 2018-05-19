using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using TinkloProblemos.API.Contracts;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class NotificationService : INotificationService
    {
        public bool SendNotification(string userId, int problemId, string content)
        {
            var client = new RestClient("https://onesignal.com/api/v1/notifications");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic NmZlYmRhZTUtMmU4NC00NTMxLTkzMjgtZDBhMTY1MDI4ODE2");
            request.AddHeader("Content-Type", "application/json");
            var notification = new Notification(userId, problemId, content);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(notification);
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return true;
            }
            //TODO Log problem
            return false;
        }
    }
}

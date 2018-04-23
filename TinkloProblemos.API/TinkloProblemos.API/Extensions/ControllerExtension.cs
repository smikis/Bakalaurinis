using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TinkloProblemos.API.Extensions
{
    /// <summary>
    /// Controller extensions
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Get Model State errors
        /// </summary>

        public static IEnumerable<string> GetModelStateErrors(this Controller controller)
        {
            return controller.ModelState.SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage);
        }
    }
}

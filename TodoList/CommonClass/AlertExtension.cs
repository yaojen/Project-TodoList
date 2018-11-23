using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoList.ViewModel;

namespace TodoList.CommonClass
{
    public static class AlertExtension
    {
        
        private static readonly string Alerts = "_Alerts";

        public static List<AlertViewModel> GetAlerts(this TempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(Alerts))
            {
                tempData[Alerts] = new List<AlertViewModel>();
            }

            return (List<AlertViewModel>)tempData[Alerts];
        }

        public static void AddAlert(this TempDataDictionary tempData,
             string messageClass, string message)
        {
            var alerts = tempData.GetAlerts();
            alerts.Add(new AlertViewModel(messageClass, message));
        }

        public static ActionResult WithSuccess(this ActionResult result,
         string message)
        {
            return new AlertDecoratorActionResult(result, "alert-success", message);
        }

        public static ActionResult WithInfo(this ActionResult result,
           string message)
        {
            return new AlertDecoratorActionResult(result, "alert-info", message);
        }

        public static ActionResult WithWarning(this ActionResult result,
             string message)
        {
            return new AlertDecoratorActionResult(result, "alert-warning", message);
        }

        public static ActionResult WithError(this ActionResult result,
           string message)
        {
            return new AlertDecoratorActionResult(result, "alert-danger", message);
        }
    }
}
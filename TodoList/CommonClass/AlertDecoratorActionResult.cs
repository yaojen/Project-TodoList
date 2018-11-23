using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TodoList.CommonClass
{
    public class AlertDecoratorActionResult : ActionResult
    {
        public ActionResult InnerResult { get; set; }
        
        public string AlertClass { get; set; }
        
        public string Message { get; set; }

        public AlertDecoratorActionResult(ActionResult inInnerResult
                                            ,string inAlertClass, string inMessage)
        {
            this.InnerResult = inInnerResult;
            this.AlertClass = inAlertClass;
            this.Message = inMessage;
        }

       
        public override void ExecuteResult(ControllerContext context)
        {
            context.Controller.TempData.AddAlert(this.AlertClass, this.Message);
            this.InnerResult.ExecuteResult(context);
        }
    }
}
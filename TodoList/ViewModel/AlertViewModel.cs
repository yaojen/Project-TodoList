using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoList.ViewModel
{
    
    public class AlertViewModel
    {
        public string AlertClass { get; set; }

        public string Message { get; set; }

        public AlertViewModel(string alertClass, string message)
        {
            AlertClass = alertClass;
            Message = message;
        }
    }
}
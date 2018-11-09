using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoList.Models;

namespace TodoList.ViewModel
{
    public class ItemsIndexViewModel
    {
        public string Subject { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool Finished { get; set; }

        public string Memo { get; set; }

    }
}
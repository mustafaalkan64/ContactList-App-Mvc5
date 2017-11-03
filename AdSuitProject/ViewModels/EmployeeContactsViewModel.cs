using AdSuit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdSuitProject.ViewModels
{
    public class EmployeeContactsViewModel
    {
        public EmployeeViewModel employee { get; set; }
        public ContactTypeViewModel[] contactTypes { get; set; }
        public TagViewModel[] tags { get; set; }
    }
}
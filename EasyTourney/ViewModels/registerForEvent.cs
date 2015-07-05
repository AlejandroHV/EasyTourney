using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTourney.Models;

namespace EasyTourney.ViewModels
{
    public class RegisterForEvent
    {
        public Guid userGuid { get; set; }
        public tblEvent selectedEvent { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTourney.Models;

namespace EasyTourney.ViewModels
{
    public class EventMessagesModel
    {
        public tblEvent currentEvent { get; set; }
        public tblUser user { get; set; }
        public string message { get; set; }
        public Guid eventId { get{
            return currentEvent.GUID;
        }
        }
    }
}
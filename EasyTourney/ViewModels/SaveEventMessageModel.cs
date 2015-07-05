using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTourney.Models;

namespace EasyTourney.ViewModels
{
    public class SaveEventMessageModel
    {
        public tblEvent relatedEvent { get; set; }
        public tblMessage message { get; set; }
    }
}
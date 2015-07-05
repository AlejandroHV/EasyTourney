using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTourney.Models;

namespace EasyTourney.ViewModels
{
    public class EventIndex
    {

        public tblEvent eventInfo { get; set; }


        public List<tblUserEvents> eventParticipants { get; set; }



    }
}
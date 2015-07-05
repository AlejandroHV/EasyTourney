using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTourney.Models;

namespace EasyTourney.Bll
{    
    public class EventBll
    {
        private  DBEasyTourneyEntities db = new DBEasyTourneyEntities();

        public  List<tblEvent> getEventsByManager(Guid managerGuid)
        {
            var query = from e in db.tblEvent
                        join ue in db.tblUserEvents on e.GUID equals ue.EventId
                        where ue.UserId == managerGuid
                        select e;

            return query.ToList();
        }
    }
}
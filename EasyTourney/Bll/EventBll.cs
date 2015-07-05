using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTourney.Models;
using EasyTourney.ViewModels;

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

        public List<EventMessagesModel> getMessagesByEvent(Guid eventGuid)
        {
            var query = from m in db.tblMessage
                        join em in db.tblEventMessages
                            on m.Id equals em.EventId
                        join e in db.tblEvent
                            on em.EventId equals e.GUID
                        join u in db.tblUser
                            on m.UserId equals u.GUID
                        where e.GUID == eventGuid
                        select new EventMessagesModel { 
                            currentEvent = e,
                            user = u,
                            message = m.Content
                        };

            return query.ToList();
        }
    }
}
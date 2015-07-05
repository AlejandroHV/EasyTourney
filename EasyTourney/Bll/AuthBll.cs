using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTourney.Models;

namespace EasyTourney.Bll
{
    public class AuthBll
    {
        public static bool isAdmin()
        {
            tblUser user = null;

            if (HttpContext.Current.Session["USER"] != null)
            {
                user = (tblUser)HttpContext.Current.Session["USER"];

                if (user.tblRol.Name == "Manager")
                    return true;              
            }

            return false;
        }

        public static bool isParticipant()
        {
            tblUser user = null;

            if (HttpContext.Current.Session["USER"] != null)
            {
                user = (tblUser)HttpContext.Current.Session["USER"];

                if (user.tblRol.Name == "Participant")
                    return true;
            }

            return false;
        }

        public static bool activeSession()
        {
            bool activeSession = HttpContext.Current.Session["USER"] != null ? true : false;
           
            return activeSession;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoneyMustard.Web.Models
{
    public class ReminderScreen
    {
      


        public List<ReminderCenter> Reminders { get; set; }
    }
   
    public class FromReminderListTableRow
    {
        public int ContactID { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
    }
    public class ToReminderListTableRow
    {
        public int ContactID { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
    }

    public class ReminderCenter
    {
        public int ReminderID { get; set; }
        public List<FromReminderListTableRow> From { get; set; }
        public List<ToReminderListTableRow> To { get; set; }

       //public int to { get; set; }
       //public int from { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public DateTime ReminderDateTime { get; set; }

    }
    public class ReminderListCenter
    {



        public List<ReminderList> Reminderss { get; set; }
    }

    public class ReminderList
    {
        public int ReminderID { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        //public int to { get; set; }
        //public int from { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public DateTime ReminderDateTime { get; set; }

    }

    public class EditReminderModel
    {
        public int ReminderID { get; set; }
        public int From { get; set; }
        public int To { get; set; }

        //public int to { get; set; }
        //public int from { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public DateTime ReminderDateTime { get; set; }

    }
        
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNPEFWEB.ViewModel
{
    public class MailClass
    {
        public string fromMailId { get; set; }
        public string fromMailPassword { get; set; }
        public List<string> toMailIds { get; set; }
        public string to { get; set; }
        public string from { get; set; }
        public string fromName { get; set; }
        public string subject { get; set; }
        public string bodyText { get; set; }
        public string message { get; set; }
        public string code { get; set; }


    }
    public class Media
    {
        public string url { get; set; }
        public string caption { get; set; }
    }

    public class smsmodel
    {
        public string to { get; set; }
        public string from { get; set; }
        public string sms { get; set; }
        public string type { get; set; }
        public string channel { get; set; }
        public string api_key { get; set; }
        public Media media { get; set; }
    }

    public class smxmodel
    {
        public string phoneNumber { get; set; }
        public string message { get; set; }
        public string from { get; set; }
        public string code { get; set; }
        //public string ApiKey { get; set; }

    }

}

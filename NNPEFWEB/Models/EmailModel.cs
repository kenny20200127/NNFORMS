using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace NNPEFWEB.Models
{ 
    public class EmailModel
	{
		public string to { get; set; }
		public string subject { get; set; }
		public string text { get; set; }
		public string html { get; set; }
		public List<IFormFile>? Attachments { get; set; }
	}
    public class EmailModel2
    {
        public EmailModel2()
        {
            //to = new List<string>();
            //cc= new List<string>();
            //bcc= new List<string>();
            Attachments= new List<IFormFile>();
        }
        
        public string to { get; set; }
        public string? bcc { get; set; }
        public string? cc { get; set; }
        public string subject { get; set; }
        public string text { get; set; }
        public string html { get; set; }
        public List<IFormFile>? Attachments { get; set; }

    }
   
}

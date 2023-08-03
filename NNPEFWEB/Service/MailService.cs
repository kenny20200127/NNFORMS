using Microsoft.Extensions.Configuration;
using NNPEFWEB.ViewModel;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;


namespace NNPEFWEB.Service
{
    public class MailService: IMailService
    {
        private readonly IConfiguration config;
        public MailService(IConfiguration config)
        {
            this.config = config;
        }
        public void SendEmail(MailClass mail)
        {
                mail.from= config.GetValue<string>("mailconfig:SenderEmail");
                var apikey = config.GetValue<string>("mailconfig:Apikey");
                var UserName = config.GetValue<string>("mailconfig:hostmails");
                WebClient client = new WebClient();
                NameValueCollection values = new NameValueCollection();
                values.Add("username", UserName);
                values.Add("api_key", apikey);
                values.Add("from", mail.from);
                values.Add("from_name", mail.fromName);
                values.Add("subject", mail.subject);
                values.Add("body_text", mail.bodyText);
                values.Add("to", mail.to);

                byte[] response = client.UploadValues("https://api.elasticemail.com/mailer/send", values);


        }
        public string sendsms(smxmodel mail)
        {
            string BASE_URL = config.GetValue<string>("smx:url");
            string API_KEY = config.GetValue<string>("smx:apikey");
            string SENDER = config.GetValue<string>("smx:SenderName");
            string RECIPIENT = mail.phoneNumber;
            string MESSAGE_TEXT = mail.message;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("App", API_KEY);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string message = $@"
            {{
                ""messages"": [
                {{
                    ""from"": ""{SENDER}"",
                    ""destinations"":
                    [
                        {{
                            ""to"": ""{RECIPIENT}""
                        }}
                  ],
                  ""text"": ""{MESSAGE_TEXT}""
                }}
              ]
            }}";

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "sms/2/text/advanced");
            httpRequest.Content = new StringContent(message, Encoding.UTF8, "application/json");

            var response = client.SendAsync(httpRequest);

            return response.Status.ToString();
        }
        public string sendemail(MailClass mail)
        {
            string BASE_URL = config.GetValue<string>("smx:url");
            string API_KEY = config.GetValue<string>("smx:apikey");
            string SENDER = config.GetValue<string>("smx:SenderName");

            string SENDER_EMAIL = config.GetValue<string>("smx:emailserver");
            string RECIPIENT_EMAIL = mail.to;
            string EMAIL_SUBJECT = config.GetValue<string>("smx:emailsubject");
            string EMAIL_TEXT = mail.bodyText;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("App", API_KEY);
            var request = new MultipartFormDataContent();
            request.Add(new StringContent(SENDER_EMAIL), "from");
            request.Add(new StringContent(RECIPIENT_EMAIL), "to");
            request.Add(new StringContent(EMAIL_SUBJECT), "subject");
            request.Add(new StringContent(EMAIL_TEXT), "text");

            var response = client.PostAsync("email/2/send", request).Result;

            var responseContent = response.Content;
            string responseString = responseContent.ReadAsStringAsync().Result;
            var responseCode = response.StatusCode;
           

            return responseCode.ToString();
        }

        public string sendsmsTermii(smxmodel mail)
        {
            var apikey = config.GetValue<string>("termiiemail:apikey");
            var senderid = config.GetValue<string>("termiiemail:senderid");
            WebClient client = new WebClient();
            NameValueCollection values = new NameValueCollection();


            values.Add("to", mail.phoneNumber);
            values.Add("from", senderid);
            values.Add("sms", mail.message);
            values.Add("type", "plain");
            values.Add("channel", "generic");
            values.Add("api_key", apikey);

            byte[] response = client.UploadValues("https://api.ng.termii.com/api/sms/send", values);

            //values.Add("api_key", apikey);
            //values.Add("message_type", "ALPHANUMERIC");
            //values.Add("to", mail.phoneNumber);
            //values.Add("from", senderid);
            //values.Add("channel", "dnd");
            //values.Add("pin_attempts", "10");
            //values.Add("pin_time_to_live", "5");
            //values.Add("pin_length", "6");
            //values.Add("pin_placeholder", mail.code);
            //values.Add("message_text", mail.message);
            //values.Add("pin_type", "NUMERIC");

           // byte[] response = client.UploadValues(" https://api.ng.termii.com/api/sms/otp/send", values);
       

            return response.ToString();
        }
        public string SendEmailTermii(MailClass mail)
        {
            var url = config.GetValue<string>("termiiemail:url");
            var apikey = config.GetValue<string>("termiiemail:apikey");
            var emailconfigid = config.GetValue<string>("termiiemail:emailconfigid");
            WebClient client = new WebClient();
            NameValueCollection values = new NameValueCollection();
            values.Add("api_key", apikey);
            values.Add("email_address", mail.to);
            values.Add("code", mail.code);
            values.Add("email_configuration_id", emailconfigid);
 

            byte[] response = client.UploadValues("https://api.ng.termii.com/api/email/otp/send", values);

            return response.ToString();
        }

        public string SendEmailsTermii(MailClass mail)
        {
            var url = config.GetValue<string>("termiiemail:url");
            var apikey = config.GetValue<string>("termiiemail:apikey");
            var emailconfigid = config.GetValue<string>("termiiemail:emailconfigid");
            WebClient client = new WebClient();
            NameValueCollection values = new NameValueCollection();
            values.Add("api_key", apikey);
            values.Add("email_address", mail.to);
            values.Add("code", mail.bodyText);
            values.Add("email_configuration_id", emailconfigid);


            byte[] response = client.UploadValues("https://api.ng.termii.com/api/email/otp/send", values);

            return response.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace CondorExtreme3.Tools
{
    public static class TwilioHelper
    {
        static string TwilioNumber = "+17792054375";
        static string AccountSid = "ACb960b03afb26043f70f5ddf899c66109";
        static string AuthToken = "bd0bf130f31dfd97aa500cc215e67738";

        public static void SendSMS(string PhoneNumber, string Message)
        {
            TwilioClient.Init(AccountSid, AuthToken);

            var message = MessageResource.Create(
                    to: new PhoneNumber(PhoneNumber),
                    from: new PhoneNumber(TwilioNumber),
                    body: Message
                );
        }
    }
}
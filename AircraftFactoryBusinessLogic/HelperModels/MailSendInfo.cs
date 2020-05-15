using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryBusinessLogic.HelperModels
{
    public class MailSendInfo
    {
        public string MailAddress { get; set; }

        public string Subject { get; set; }
        public string Text { get; set; }
    }
}

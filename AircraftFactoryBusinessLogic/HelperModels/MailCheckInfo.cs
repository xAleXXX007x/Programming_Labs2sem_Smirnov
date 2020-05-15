using AircraftFactoryBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftFactoryBusinessLogic.HelperModels
{
    public class MailCheckInfo
    {
        public string PopHost { get; set; }
        public int PopPort { get; set; }
        public IMessageInfoLogic Logic { get; set; }
    }

}

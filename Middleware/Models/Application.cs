using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Middleware.Models
{
    [XmlRoot("application")]
    public class Application
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("creation_dt")]
        public DateTime Creation_dt { get; set; }
        [XmlElement("res_type")]
        public string Res_type { get; set; }
    }
}
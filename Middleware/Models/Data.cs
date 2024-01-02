using System;
using System.Xml.Serialization;

namespace Middleware.Models
{
    [XmlRoot("data")]
    public class Data
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("content")]
        public string Content { get; set; }
        [XmlElement("creation_dt")]
        public DateTime Creation_dt { get; set; }
        [XmlElement("parent")]
        public int Parent { get; set; }
        [XmlElement("res_type")]
        public string Res_type { get; set; }
    }
}
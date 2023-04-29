using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Levels
{
    [XmlRoot(ElementName = "Creep")]
    public class Creep
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "amount")]
        public string Amount { get; set; }
    }

    [XmlRoot(ElementName = "Wave")]
    public class Wave
    {
        [XmlElement(ElementName = "Creep")]
        public List<Creep> Creeps { get; set; }
    }

    [XmlRoot(ElementName = "Level")]
    public class Level
    {
        [XmlElement(ElementName = "Wave")]
        public List<Wave> Waves { get; set; }
    }
}



﻿using System;
using System.Xml.Serialization;

namespace ContactManagerWPF
{
    public enum TLink
    {
        [XmlEnum(Name = "Friend")]
        Friend,

        [XmlEnum(Name = "Colleague")]
        Colleague,

        [XmlEnum(Name = "Relation")]
        Relation,

        [XmlEnum(Name = "Network")]
        Network,

        [XmlEnum(Name = "Unknown")]
        Unknown
    }
}

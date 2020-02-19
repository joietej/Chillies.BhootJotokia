using System;
using System.Xml.Linq;

namespace Chillies.BhootJotokia.Extensions
{
    public static class XElementExtensions
    {
        public static string Atr(this XElement elemnt, string name) =>
           elemnt.Attribute(XName.Get(name)).Value;

        public static float AtrAsFloat(this XElement elemnt, string name) =>
            float.TryParse(elemnt.Attribute(XName.Get(name)).Value, out var value)
            ? value
            : default;

        public static int AtrAsInt(this XElement elemnt, string name) =>
           int.TryParse(elemnt.Attribute(XName.Get(name)).Value, out var value)
           ? value
           : default;

        public static Guid AtrAsGuid(this XElement elemnt, string name) =>
           Guid.TryParse(elemnt.Attribute(XName.Get(name)).Value, out var value)
           ? value
           : default;
    }
}
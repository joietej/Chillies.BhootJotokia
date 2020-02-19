using System;
using System.Globalization;
using System.Xml.Linq;

namespace Chillies.BhootJotokia.Extensions
{
    public static class XElementExtensions
    {
        public static string Atr(this XElement element, string name) =>
           element.Attribute(XName.Get(name)).Value;

        public static float AtrAsFloat(this XElement element, string name) =>
            float.TryParse(element.Attribute(XName.Get(name)).Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)
            ? value
            : default;

        public static int AtrAsInt(this XElement element, string name) =>
           int.TryParse(element.Attribute(XName.Get(name)).Value, out var value)
           ? value
           : default;

        public static Guid AtrAsGuid(this XElement element, string name) =>
           Guid.TryParse(element.Attribute(XName.Get(name)).Value, out var value)
           ? value
           : default;
    }
}
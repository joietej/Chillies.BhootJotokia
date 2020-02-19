using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Chillies.BhootJotokia.Core
{
    public class XmlProvider : IXmlProvider
    {
        public async Task<T> LoadXmlAsAsync<T>(string path, string? root = null)
        {
            var xmlAsText = await File.ReadAllTextAsync(path);

            var xml = XElement.Parse(xmlAsText);

            if (xml.IsEmpty)
            {
                throw new Exception("Invalid Xml");
            }

            var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(root ?? typeof(T).Name.ToLower()));

            return (T)serializer.Deserialize(xml.CreateReader());
        }
    }
}
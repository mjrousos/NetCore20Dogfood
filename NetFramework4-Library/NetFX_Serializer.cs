using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetFramework4_Library
{
    public static class Serializer
    {
        static BinaryFormatter Formatter = new BinaryFormatter();

        public static byte[] Serialize(object o)
        {
            using (var ms = new MemoryStream())
            {
                Formatter.Serialize(ms, o);
                return ms.ToArray();
            }
        }
    }
}

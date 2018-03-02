using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Aolh.Library.Serialization.Binary
{
    public class Serializer
    {
        public static byte[] Serialize(object o)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, o);
            byte[] ret = stream.ToArray();
            stream.Close();
            return ret;
        }

        public static object Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            IFormatter formatter = new BinaryFormatter();
            object o = formatter.Deserialize(stream);
            stream.Close();
            return o;
        }
    }
}

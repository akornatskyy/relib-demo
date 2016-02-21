using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

using ReusableLibrary.Abstractions.Cryptography;
using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Abstractions.Net;
using ReusableLibrary.Abstractions.Serialization.Formatters;
using ReusableLibrary.Memcached.Protocol;

namespace Public.WebMvc.Helpers
{
    public sealed class MemcachedFactory : BinaryProtocolFactory
    {
        public MemcachedFactory(IClientFactory clientFactory, ProtocolOptions options)
            : base(clientFactory, options)
        {
        }

        public override IEncoder CreateEncoder()
        {
            ////return new ValidKeyEncoder(new TextEncoder(Encoding.UTF8));
            return new Base64Encoder(
                new HashEncoder(new HashAlgorithmProvider<SHA256Managed>(),
                    new TextEncoder(Encoding.UTF8)));
        }

        public override IObjectFormatter CreateObjectFormatter()
        {
            return new NullObjectFormatter(new DeflateObjectFormatter(
                new SymmetricObjectFormatter(new SymmetricAlgorithmProvider<RijndaelManaged>(
                        new RijndaelKeyVectorProvider("P@ssw0rd", 128)),
                    new ArrayObjectFormatter(
                new SimpleObjectFormatter(Encoding.UTF8,
                    new RuntimeObjectFormatter(new BinaryFormatter(), null))))));
        }
    }
}
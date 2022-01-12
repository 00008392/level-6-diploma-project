using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Authentication
{
    public static class JWTKeyEncoder
    {
        public static byte[] EncodeKey(string key)
        {
            return Encoding.ASCII.GetBytes(key);
        }
    }
}

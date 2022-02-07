using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Authentication
{
    public static class JWTKeyEncoder
    {
        //method for encoding security key
        public static byte[] EncodeKey(string key)
        {
            return Encoding.ASCII.GetBytes(key);
        }
    }
}

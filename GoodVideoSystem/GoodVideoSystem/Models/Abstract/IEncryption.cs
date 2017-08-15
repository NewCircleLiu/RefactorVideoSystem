using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodVideoSystem.Models.Abstract
{
    public interface IEncryption
    {
        string MyMD5(string data);
        string SHA1(string data);
        string SHA256(string data);
        string SHA348(string data);
        string SHA512(string data);
    }
}

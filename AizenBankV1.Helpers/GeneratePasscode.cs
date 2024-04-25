using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.Helpers
{
    public class GeneratePasscode
    {
        static public string Generate(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            var randomCode = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomCode[i] = chars[random.Next(chars.Length)];
            }

            return new String(randomCode);
        }
    }
}
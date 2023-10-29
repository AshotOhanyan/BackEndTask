using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.OtherMethods
{
    public static class StaticMethods
    {
        public static string FormatAddress(string inputAddress)
        {

            string formattedAddress = inputAddress.Replace(".", " ");


            formattedAddress = System.Text.RegularExpressions.Regex.Replace(formattedAddress, @"\s+", " ");

            formattedAddress = formattedAddress
                .Replace(" Avenue", " AVE")
                .Replace(" Road", " RD")
                .Replace(" Street", " ST")
                .Replace(" Drive", " DR")
                .Replace(" Boulevard", " BLVD")
                .Replace(" Apartment", " APT")
                .Replace(" Number", " ")
                .Replace(" No ", " ")
                .Replace(" #", " ");

            formattedAddress = formattedAddress.Trim().ToUpper();

            return formattedAddress;
        }
    }
}

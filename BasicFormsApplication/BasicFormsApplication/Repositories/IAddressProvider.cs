using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFormsApplication.Repositories
{
    public interface IAddressProvider
    {
        (string, string) GetProvinceAndStreet(string postalCode, string houseNumber);
        (string, string) GetPostalCodeAndHouseNumber(string ipAddress);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicFormsApplication.Models.Forms;
using BasicFormsApplication.Repositories;
using BasicFormsApplication.UserContextProvider;

namespace BasicFormsApplication.Strategies
{
    public class AddressFormStrategy : BaseFormStrategy
    {
        private readonly IUserContextProvider _userContextProvider;
        private readonly IAddressProvider _addressProvider;

        public AddressFormStrategy(IUserContextProvider userContextProvider,
            IAddressProvider addressProvider)
        {
            _userContextProvider = userContextProvider;
            _addressProvider = addressProvider;
        }

        public override void AddPrefillData(Dictionary<string, object> preFillDictionary)
        {
            base.AddPrefillData(preFillDictionary);

            var user = _userContextProvider.GetCurrentAuthenticatedUser();
            if (user != null)
            {
                var (postalCode, houseNumber) = _addressProvider.GetPostalCodeAndHouseNumber(user.IpAddress);
                AddPrefillValueToDictionary(preFillDictionary, nameof(AddressForm.PostalCode), postalCode);
                AddPrefillValueToDictionary(preFillDictionary, nameof(AddressForm.HouseNumber), houseNumber);
            }
        }
    }
}

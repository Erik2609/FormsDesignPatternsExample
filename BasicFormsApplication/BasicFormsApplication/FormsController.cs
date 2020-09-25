using BasicFormsApplication.Models.Forms;
using BasicFormsApplication.Repositories;
using BasicFormsApplication.UserContextProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFormsApplication
{
    public class FormsController
    {
        private readonly IFormRepository _formRepository;
        private readonly IUserContextProvider _userContextProvider;
        private readonly IAddressProvider _addressProvider;

        public FormsController(IFormRepository formRepository,
            IUserContextProvider userContextProvider,
            IAddressProvider addressProvider)
        {
            _formRepository = formRepository;
            _userContextProvider = userContextProvider;
            _addressProvider = addressProvider;
        }

        public Dictionary<string, object> GetPrefillData(string formName)
        {
            var preFillDictionary = new Dictionary<string, object>();
            var form = _formRepository.GetFormDefinition(formName);
            var user = _userContextProvider.GetCurrentAuthenticatedUser();

            if (form.FormName == AddressForm.FORM_NAME)
            {
                if (user != null)
                {
                    var (postalCode, houseNumber) = _addressProvider.GetPostalCodeAndHouseNumber(user.IpAddress);
                    AddPrefillValueToDictionary(preFillDictionary, nameof(AddressForm.PostalCode), postalCode);
                    AddPrefillValueToDictionary(preFillDictionary, nameof(AddressForm.HouseNumber), houseNumber);
                }
            }
            else if(form.FormName == PersonalInformationForm.FORM_NAME)
            {
                if (user != null)
                {
                    AddPrefillValueToDictionary(preFillDictionary, nameof(PersonalInformationForm.Name), user.Name);
                }
            }

            return preFillDictionary;
        }

        public bool SubmitForm(IForm form)
        {
            var user = _userContextProvider.GetCurrentAuthenticatedUser();
            if (form.FormName == AddressForm.FORM_NAME)
            {
                var (province, street) = _addressProvider.GetProvinceAndStreet(form.SubmittedValues[nameof(AddressForm.PostalCode)]?.ToString(),
                    form.SubmittedValues[nameof(AddressForm.HouseNumber)]?.ToString());

                form.SubmittedValues[nameof(AddressForm.Province)] = province;
                form.SubmittedValues[nameof(AddressForm.Street)] = street;

                if (user != null)
                {
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserEmail)] = user.Email;
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserId)] = user.Id;
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserIpAddress)] = user.IpAddress;
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserName)] = user.Name;
                }
            }
            else if (form.FormName == PersonalInformationForm.FORM_NAME)
            {
                if (user != null)
                {
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserEmail)] = user.Email;
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserId)] = user.Id;
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserIpAddress)] = user.IpAddress;
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserName)] = user.Name;
                }
            }

            return _formRepository.Submit(form);
        }

        private void AddPrefillValueToDictionary(Dictionary<string, object> preFillDictionary, string key, string value)
        {
            if(preFillDictionary.ContainsKey(key))
            {
                preFillDictionary[key] = value;
            }
            else
            {
                preFillDictionary.Add(key, value);
            }
        }
    }
}

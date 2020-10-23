using BasicFormsApplication.Models.Forms;
using BasicFormsApplication.Repositories;
using BasicFormsApplication.Strategies;
using BasicFormsApplication.UserContextProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFormsApplication.Factory
{
    public static class FormLogicStrategyFactory
    {
        public static IFormLogicStrategy GetFormLogicStrategy(string formName,
            IUserContextProvider userContextProvider,
            IAddressProvider addressProvider)
        {
            var strategy = new BaseFormStrategy();
            switch(formName)
            {
                case AddressForm.FORM_NAME:
                    strategy = new AddressFormStrategy(userContextProvider, addressProvider);
                    break;
                case PersonalInformationForm.FORM_NAME:
                    strategy = new PersonalFormInformationStrategy(userContextProvider);
                    break;
                default:
                    break;
            }

            return strategy;
        }
    }
}

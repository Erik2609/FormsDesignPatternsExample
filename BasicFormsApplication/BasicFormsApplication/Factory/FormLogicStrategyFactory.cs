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
        public static IFormLogicStrategy GetFormLogicStrategy(IForm form,
            IUserContextProvider userContextProvider,
            IAddressProvider addressProvider)
        {
            IFormLogicStrategy strategy = new BaseFormStrategy();
            switch(form.FormName)
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

            if(AuditFormLogicDecorator.IsFormAuditable(form))
            {
                strategy = new AuditFormLogicDecorator(strategy, userContextProvider);
            }
            
            return strategy;
        }
    }
}

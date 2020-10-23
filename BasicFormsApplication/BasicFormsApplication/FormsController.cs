using BasicFormsApplication.Factory;
using BasicFormsApplication.Models.Forms;
using BasicFormsApplication.Repositories;
using BasicFormsApplication.Strategies;
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

            var formLogicStrategy = GetFormLogicStrategy(form);
            formLogicStrategy.AddPrefillData(preFillDictionary);

            return preFillDictionary;
        }

        public bool SubmitForm(IForm form)
        {
            var formLogicStrategy = GetFormLogicStrategy(form);
            formLogicStrategy.AddOnPostData(form);

            return _formRepository.Submit(form);
        }

        public IFormLogicStrategy GetFormLogicStrategy(IForm form)
        {
            return FormLogicStrategyFactory.GetFormLogicStrategy(form, _userContextProvider, _addressProvider);
        }

    }
}

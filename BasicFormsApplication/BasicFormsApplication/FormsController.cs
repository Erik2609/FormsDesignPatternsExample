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
        private readonly IUserContexProvider _userContexProvider;
        private readonly IAddressProvider _addressProvider;

        public FormsController(IFormRepository formRepository,
            IUserContexProvider userContexProvider,
            IAddressProvider addressProvider)
        {
            _formRepository = formRepository;
            _userContexProvider = userContexProvider;
            _addressProvider = addressProvider;
        }

        public Dictionary<string, object> GetPrefillData(string formName)
        {
            var prefillObject = new Dictionary<string, object>();
            var form = _formRepository.GetFormDefinition(formName);

            // TODO Assign Prefill values

            return prefillObject;
        }

        public bool SubmitForm(IForm form)
        {
            // TODO Assign specific form values

            return _formRepository.Submit(form);
        }
    }
}

using BasicFormsApplication.Models.Forms;
using BasicFormsApplication.Repositories;
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
        public FormsController(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }

        public IForm GetPrefillData(string formName)
        {
            var form = _formRepository.GetForm(formName);

            // TODO Assign Prefill values

            return form;
        }

        public bool SubmitForm(IForm form)
        {
            // TODO Assign specific form values

            return _formRepository.Submit(form);
        }
    }
}

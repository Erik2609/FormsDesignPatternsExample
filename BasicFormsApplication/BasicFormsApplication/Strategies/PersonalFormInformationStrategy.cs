using BasicFormsApplication.Models.Forms;
using BasicFormsApplication.UserContextProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFormsApplication.Strategies
{
    public class PersonalFormInformationStrategy : BaseFormStrategy
    {
        private readonly IUserContextProvider _userContextProvider;

        public PersonalFormInformationStrategy(IUserContextProvider userContextProvider)
        {
            _userContextProvider = userContextProvider;
        }

        public override void AddPrefillData(Dictionary<string, object> preFillDictionary)
        {
            base.AddPrefillData(preFillDictionary);
            var user = _userContextProvider.GetCurrentAuthenticatedUser();
            if (user != null)
            {
                AddPrefillValueToDictionary(preFillDictionary, nameof(PersonalInformationForm.Name), user.Name);
            }
        }
    }
}

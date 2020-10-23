using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicFormsApplication.Models.Forms;
using BasicFormsApplication.UserContextProvider;

namespace BasicFormsApplication.Strategies
{
    public class AuditFormLogicDecorator : IFormLogicStrategy
    {
        private readonly IFormLogicStrategy _formLogicStrategy;
        private readonly IUserContextProvider _userContextProvider;

        public AuditFormLogicDecorator(IFormLogicStrategy formLogicStrategy,
            IUserContextProvider userContextProvider)
        {
            _formLogicStrategy = formLogicStrategy;
            _userContextProvider = userContextProvider;
        }

        public void AddOnPostData(IForm form)
        {
            _formLogicStrategy.AddOnPostData(form);

            var user = _userContextProvider.GetCurrentAuthenticatedUser();
            if (user != null)
            {
                form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserEmail)] = user.Email;
                form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserId)] = user.Id;
                form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserIpAddress)] = user.IpAddress;
                form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserName)] = user.Name;
            }
        }

        public void AddPrefillData(Dictionary<string, object> preFillDictionary)
        {
            _formLogicStrategy.AddPrefillData(preFillDictionary);
        }


        private static readonly string[] AuditFields = new string[]
        {
            nameof(IAuditInformation.AuthenticatedUserEmail),
            nameof(IAuditInformation.AuthenticatedUserId),
            nameof(IAuditInformation.AuthenticatedUserIpAddress),
            nameof(IAuditInformation.AuthenticatedUserName)
        };
        public static bool IsFormAuditable(IForm form)
        {
            return form.SubmittedValues.Keys.Any(field => AuditFormLogicDecorator.AuditFields.Contains(field));
        }
    }
}

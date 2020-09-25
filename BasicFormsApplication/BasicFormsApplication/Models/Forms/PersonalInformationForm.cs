using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFormsApplication.Models.Forms
{
    public class PersonalInformationForm : IForm, IAuditInformation
    {
        public string FormName { get; } = "Personal";
        public Dictionary<string, object> SubmittedValues { get; } = new Dictionary<string, object>()
        {
            { nameof(Name), null },
            { nameof(DateOfBirth), null },
            { nameof(AuthenticatedUserId), null },
            { nameof(AuthenticatedUserEmail), null },
            { nameof(AuthenticatedUserName), null },
            { nameof(AuthenticatedUserIpAddress), null },
        };

        public string Name
        {
            get
            {
                return SubmittedValues[nameof(Name)] as string;
            }
            set
            {
                SubmittedValues[nameof(Name)] = value;
            }
        }


        public DateTime? DateOfBirth
        {
            get
            {
                return SubmittedValues[nameof(DateOfBirth)] as DateTime?;
            }
            set
            {
                SubmittedValues[nameof(DateOfBirth)] = value;
            }
        }

        public int AuthenticatedUserId
        {
            get
            {
                return SubmittedValues[nameof(AuthenticatedUserId)] as int? ?? 0;
            }
            set
            {
                SubmittedValues[nameof(AuthenticatedUserId)] = value;
            }
        }
        public string AuthenticatedUserEmail
        {
            get
            {
                return SubmittedValues[nameof(AuthenticatedUserEmail)] as string;
            }
            set
            {
                SubmittedValues[nameof(AuthenticatedUserEmail)] = value;
            }
        }
        public string AuthenticatedUserName
        {
            get
            {
                return SubmittedValues[nameof(AuthenticatedUserName)] as string;
            }
            set
            {
                SubmittedValues[nameof(AuthenticatedUserName)] = value;
            }
        }
        public string AuthenticatedUserIpAddress
        {
            get
            {
                return SubmittedValues[nameof(AuthenticatedUserIpAddress)] as string;
            }
            set
            {
                SubmittedValues[nameof(AuthenticatedUserIpAddress)] = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFormsApplication.Models.Forms
{
    public class AddressForm : IForm, IAuditInformation
    {
        public string FormName { get; } = "Address";
        public Dictionary<string, object> SubmittedValues { get; } = new Dictionary<string, object>()
        {
            { nameof(PostalCode), null },
            { nameof(Street), null },
            { nameof(HouseNumber), null },
            { nameof(Province), null },
            { nameof(AuthenticatedUserId), null },
            { nameof(AuthenticatedUserEmail), null },
            { nameof(AuthenticatedUserName), null },
            { nameof(AuthenticatedUserIpAddress), null },
        };

        public string PostalCode
        {
            get
            {
                return SubmittedValues[nameof(PostalCode)] as string;
            }
            set
            {
                SubmittedValues[nameof(PostalCode)] = value;
            }
        }

        public string HouseNumber
        {
            get
            {
                return SubmittedValues[nameof(HouseNumber)] as string;
            }
            set
            {
                SubmittedValues[nameof(HouseNumber)] = value;
            }
        }

        public string Street
        {
            get
            {
                return SubmittedValues[nameof(Street)] as string;
            }
            set
            {
                SubmittedValues[nameof(Street)] = value;
            }
        }

        public string Province
        {
            get
            {
                return SubmittedValues[nameof(Province)] as string;
            }
            set
            {
                SubmittedValues[nameof(Province)] = value;
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

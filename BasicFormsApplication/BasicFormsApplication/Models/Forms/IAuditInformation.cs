using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFormsApplication.Models.Forms
{
    public interface IAuditInformation
    {
        int AuthenticatedUserId { get; set; }
        string AuthenticatedUserEmail { get; set; }
        string AuthenticatedUserName { get; set; }
        string AuthenticatedUserIpAddress { get; set; }
    }
}

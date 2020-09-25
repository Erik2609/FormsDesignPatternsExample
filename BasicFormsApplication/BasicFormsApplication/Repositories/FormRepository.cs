using BasicFormsApplication.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFormsApplication.Repositories
{
    public interface IFormRepository
    {
        IForm GetForm(string formName);
        bool Submit(IForm form);
        
    }
}

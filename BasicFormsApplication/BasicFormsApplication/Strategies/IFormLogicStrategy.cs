using BasicFormsApplication.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFormsApplication.Strategies
{
    public interface IFormLogicStrategy
    {
        void AddPrefillData(Dictionary<string, object> preFillDictionary);
        void AddOnPostData(IForm form);
    }
}

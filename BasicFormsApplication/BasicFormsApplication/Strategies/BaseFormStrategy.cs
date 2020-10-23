using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicFormsApplication.Models.Forms;

namespace BasicFormsApplication.Strategies
{
    public class BaseFormStrategy : IFormLogicStrategy
    {
        public virtual void AddOnPostData(IForm form)
        {
        }

        public virtual void AddPrefillData(Dictionary<string, object> preFillDictionary)
        {
        }

        protected void AddPrefillValueToDictionary(Dictionary<string, object> preFillDictionary, string key, string value)
        {
            if (preFillDictionary.ContainsKey(key))
            {
                preFillDictionary[key] = value;
            }
            else
            {
                preFillDictionary.Add(key, value);
            }
        }
    }
}

using BasicFormsApplication.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFormsApplication.UserContextProvider
{
    public interface IUserContexProvider
    {
        User GetCurrentAuthenticatedUser();
    }
}

using ITCMS_HUIT.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCMS_HUIT.Repository.Interfaces
{
    public interface IAccountRepo
    {
        public Task<IdentityResult> RegisterAdmin(Register model);
        public Task<IdentityResult> RegisterTeacher(Register model);
        public Task<string> ChangedPassword(ChangePassword model);
        public Task<string> LogIn(Login model);
        public Task LogOut();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nitkin.AnkhRemine.Domain;

namespace Nitkin.AnkhRedmine.Dal.Interfaces
{
    public interface IUsersLocalSettingsDao
    {
        UsersLocalSettings GetUsersLocalSettings(out Exception error);
        void SaveUsersLocalSettings(UsersLocalSettings settings);
    }
}

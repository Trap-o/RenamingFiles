using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RandomNamesWithUI.lib.interfaces
{
    internal interface IValidateName
    {
        protected string ValidateName(TextBox newName);
    }
}

using System.Windows.Controls;

namespace RandomNamesWithUI.lib.interfaces
{
    internal interface IValidateName
    {
        protected static abstract string ValidateName(string newName);
    }
}

namespace RandomNamesWithUI.lib.interfaces
{
    public interface IConfigurableDialog
    {
        bool Multiselect { get; set; }
        string Title { get; set; }
        bool? ShowDialog();
    }
}

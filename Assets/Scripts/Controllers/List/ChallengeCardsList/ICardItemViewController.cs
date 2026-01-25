namespace UserInterface.Views
{
    public interface ICardItemViewController
    {
        void Clear();
        void SelectionToggled(bool isOn);
        void ItemClicked();
        void DeleteClicked();
    }
}
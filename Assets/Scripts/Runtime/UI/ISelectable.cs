namespace UI
{
    public interface ISelectable
    {
        bool IsHighlighted { get; }
        void Highlight();
        void Dehighlight();
        void Select();
    }
}
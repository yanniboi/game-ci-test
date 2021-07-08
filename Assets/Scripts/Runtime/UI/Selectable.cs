using System;

namespace UI
{
    public class Selectable : Interactable, ISelectable
    {
        public bool IsHighlighted { get; protected set; }
        public event Action OnHighlight;
        public event Action OnDehighlight;
        public event Action OnSelect;

        public void Highlight()
        {
            IsHighlighted = true;
            if (OnHighlight != null)
            {
                OnHighlight();
            }
        }
        
        public void Dehighlight()
        {
            IsHighlighted = false;
            if (OnDehighlight != null)
            {
                OnDehighlight();
            }
        }

        public void Select()
        {
            if (OnSelect != null)
            {
                OnSelect();
            }
            Dehighlight();
        }
    }
}
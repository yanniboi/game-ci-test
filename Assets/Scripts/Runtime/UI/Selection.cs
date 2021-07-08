using System.Collections.Generic;

namespace UI
{
    public class Selection
    {
        private ISelectable[] _selectables;
        
        public Selection(ISelectable selectable)
        {
            selectable.Highlight();
            _selectables = new[] {selectable};
        }

        public Selection(ISelectable[] selectables)
        {
            selectables[0].Highlight();
            _selectables = selectables;
        }

        public void Scroll()
        {
            for (int i = 0; i < _selectables.Length; i++)
            {
                if (_selectables[i].IsHighlighted)
                {
                    _selectables[i].Dehighlight();

                    int j = (i == (_selectables.Length - 1)) ? 0 : i + 1 ;
                    _selectables[j].Highlight();
                    break;
                }
            }
        }

        public ISelectable MakeSelection()
        {
            for (int i = 0; i < _selectables.Length; i++)
            {
                if (_selectables[i].IsHighlighted)
                {
                    _selectables[i].Select();
                    return _selectables[i];
                }
            }

            throw new KeyNotFoundException("No selectable is currently highlighted.");
        }
    }
}
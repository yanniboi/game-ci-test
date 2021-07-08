using System.Collections.Generic;

namespace UI
{
    public class Interaction
    {
        private List<Interactable> _interactables = new List<Interactable>();
            
        public Interaction(Interactable interactable)
        {
            _interactables.Add(interactable);
        }
            
        public Interaction(List<Interactable> interactables)
        {
            _interactables = interactables;
        }
        
        public Interaction(List<Identity> identities)
        {
            List<Interactable> interactables = new List<Interactable>();
            foreach (var identity in identities)
            {
                interactables.Add(identity);
            }
            _interactables = interactables;
        }
            
        public Interaction() : this( new List<Interactable>()) { }

        public bool IsInteracting { get; protected set; }

        public void Interact()
        {
            IsInteracting = _interactables.Count == 1;
        }
    }
}
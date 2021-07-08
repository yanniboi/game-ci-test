using System;

namespace UI
{
    public class Interactable
    {

        public event Action OnInteraction;


        public void Interact()
        {
            if (OnInteraction != null)
            {
                OnInteraction();
            }
        }
    }
}
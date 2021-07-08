
namespace UI
{
    public class Callout
    {


        public Callout(ICalloutData data)
        {
            Name = data.GetName();
            Text = data.GetText();
        }

        public Callout()
        {
        }


        public bool IsShowing { get; protected set; }
        public string Name { get; protected set; }
        public string Text { get; protected set; }

        protected void ShowCallout()
        {
            IsShowing = true;
        }
        
        public void SetInteraction(Interactable interactable)
        {
            interactable.OnInteraction += ShowCallout;
        }
    }
}
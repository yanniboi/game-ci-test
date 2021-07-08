namespace NPC.Actions
{
    public enum ActionState
    {
        pending,
        started,
        completed
    };

    public abstract class ActionBase
    {
        public ActionState CurrentState; 
    
        protected ActionBase()
        {
            CurrentState = ActionState.pending;
        }

        public virtual void Prepare()
        {
            CurrentState = ActionState.started;
        }
    
        public virtual void Complete()
        {
            CurrentState = ActionState.completed;
        }

        public abstract void Process();

    }
}
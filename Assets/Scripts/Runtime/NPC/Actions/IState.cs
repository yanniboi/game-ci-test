namespace NPC.Actions
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}
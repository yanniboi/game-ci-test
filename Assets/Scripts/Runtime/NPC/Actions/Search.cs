namespace NPC.Actions
{
    public class Search : IState
    {
        private readonly IStateController _controller;
        private int _waitTime = 100;
        private int _currentTime = 0;

        public Search(IStateController controller)
        {
            _controller = controller;
        }

        public void Tick()
        {
            _controller.SetTarget(_controller.FindNewTarget());
        }
        
        

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}
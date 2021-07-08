namespace NPC.Actions
{
    public class Wait : IState
    {
        private readonly IStateController _controller;
        private int _waitTime = 100;
        private int _currentTime = 0;

        public Wait(IStateController controller)
        {
            _controller = controller;
        }

        public void Tick()
        {
            if (_currentTime < _waitTime)
            {
                _currentTime++;
                return;
            }
        }
        
        

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}
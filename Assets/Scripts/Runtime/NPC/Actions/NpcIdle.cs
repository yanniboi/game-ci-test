using UnityEngine;

namespace NPC.Actions
{
    public class NpcIdle : IState
    {
        private readonly IStateController _controller;

        public NpcIdle(IStateController controller)
        {
            _controller = controller;
        }

        public void Tick()
        {
            return;
        }
        
        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}
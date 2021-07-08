using UnityEngine;

namespace NPC.Actions
{
    public class Idle : IState
    {
        private readonly PlayerSmImplementation _player;
        private int _waitTime = 100;
        private int _currentTime = 0;

        public Idle(PlayerSmImplementation player)
        {
            _player = player;
        }

        public void Tick()
        {
            if (_currentTime < _waitTime)
            {
                _currentTime++;
                return;
            }

            Debug.Log("position changed");
            
            _player.Target = Vector2.up;
            Debug.Log(_player.transform.position);
            Debug.Log(_player.Target);
        }
        
        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}
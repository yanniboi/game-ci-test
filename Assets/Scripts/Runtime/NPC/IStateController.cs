using UnityEngine;

namespace NPC
{
    public interface IStateController
    {

        float GetSpeed();

        Vector2 GetTarget();
        Vector2 GetDirection();
        
        void SetTarget(Vector2 target);

        Vector2 FindNewTarget();

    }
}
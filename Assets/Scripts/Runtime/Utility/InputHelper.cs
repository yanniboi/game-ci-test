using UnityEngine;

namespace Utility
{
    public class InputHelper : IInputHelper
    {
        public Vector2 GetDirectionOfTravel()
        {
            
            Vector2 directionOfTravel = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            return directionOfTravel;
        }
    }

    public interface IInputHelper
    {
        Vector2 GetDirectionOfTravel();
    }
}
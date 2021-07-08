using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace NPC
{
    public class NpcDirection : MonoBehaviour
    {

        // Direction definitions.
        [Flags]
        public enum DirectionType
        {
            None = 0,
            Up = 1,
            Down = 2,
            Left = 4,
            Right = 8
        }
     
        // Direction UI component.
        public  DirectionType allowedDirection;

        // Map the directions onto vectors.
        private Vector2[] _directionMap = new Vector2[9];

    
        // Helper variables for arrow drawing.
        private int _arrowDirectionSize = 3;
        private float _arrowHeadLength = 1f;
        private Color _arrowColor = Color.blue;
    
        private void Start()
        {
            // Set up the direction map from enum to vector.
            _directionMap[0] = Vector2.zero;
            _directionMap[1] = Vector2.up;
            _directionMap[2] = Vector2.down;
            _directionMap[4] = Vector2.left;
            _directionMap[8] = Vector2.right;
        }

        public Vector2 GetDirectionVector(DirectionType directionType)
        {
            return _directionMap[(int) directionType];
        }

        public List<Vector2> GetAllowedDirections()
        {
 
            List<Vector2> allowedDirectionVectors = new List<Vector2>();

            var directionTypes = Enum.GetValues(typeof(DirectionType));
            foreach (DirectionType value in directionTypes)
            {
                if ((allowedDirection & value) == value)
                {
                    if (value == DirectionType.None)
                    {
                        continue;
                    }
                    allowedDirectionVectors.Add(GetDirectionVector(value));
                }
            }

            return allowedDirectionVectors;
        }
    
        private void OnDrawGizmos()
        {
            var checkTypeValues = Enum.GetValues(typeof(DirectionType));
            foreach (DirectionType value in checkTypeValues)
            {
                if ((allowedDirection & value) == value)
                {
                    switch (value)
                    {
                        case DirectionType.Up:
                            DrawArrows.ForGizmo(transform.position, Vector3.up * _arrowDirectionSize, _arrowColor, _arrowHeadLength);
                            break;
                        case DirectionType.Down:
                            DrawArrows.ForGizmo(transform.position, Vector3.down * _arrowDirectionSize, _arrowColor, _arrowHeadLength);
                            break;
                        case DirectionType.Left:
                            DrawArrows.ForGizmo(transform.position, Vector3.left * _arrowDirectionSize, _arrowColor, _arrowHeadLength);
                            break;
                        case DirectionType.Right:
                            DrawArrows.ForGizmo(transform.position, Vector3.right * _arrowDirectionSize, _arrowColor, _arrowHeadLength);
                            break;
                    }
                }
            }
        }

    }
}

using System;
using UnityEngine;

namespace NPC
{
    public class DespawnScript : MonoBehaviour
    {
        public event Action OnNpcDespawned;

        public void NpcDespawned()
        {
            // Invoke observers.
            if (OnNpcDespawned != null)
            {
                OnNpcDespawned();
            }
        }
    }
}

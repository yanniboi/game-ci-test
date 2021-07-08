using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPC
{
    public class SpawnScript : MonoBehaviour
    {

        public GameObject npc;
        public float spawnTime = 15f;
        public AnimatorOverrideController[] npcAnimatorControllers;
    
        public event Action OnNpcSpawned;
    
        void Start ()
        {
            // SpawnNpc();
            InvokeRepeating ("SpawnNpc", spawnTime, spawnTime);
        }

        void SpawnNpc()
        {
            int i = Random.Range(0, npcAnimatorControllers.Length);
            npc.GetComponent<Animator>().runtimeAnimatorController = npcAnimatorControllers[i];
            Instantiate(npc, transform.position, Quaternion.identity);

            // Invoke observers.
            if (OnNpcSpawned != null)
            {
                OnNpcSpawned();
            }
        }
    }
}

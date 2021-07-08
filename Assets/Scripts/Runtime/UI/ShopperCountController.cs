using NPC;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ShopperCountController : MonoBehaviour
    {
        private ShopperCounter _counter;  
        private TextMeshProUGUI _tmp;
        private SpawnScript _spawner;
        private DespawnScript _despawner;

        // Start is called before the first frame update
        void Start()
        {
            _counter = new ShopperCounter();
            _tmp = GetComponent<TextMeshProUGUI>();
            _tmp.text = _counter.Count.ToString();
        }

        private void IncrementCounter()
        {
            _counter.Increase();
            _tmp.text = _counter.Count.ToString();
        }
        
        private void DescreaseCounter()
        {
            _counter.Decrease();
            _tmp.text = _counter.Count.ToString();
        }
    
        private void OnEnable()
        {
            _spawner = FindObjectOfType<SpawnScript>();
            if (_spawner)
            {
                _spawner.OnNpcSpawned += IncrementCounter;
            }
            _despawner = FindObjectOfType<DespawnScript>();
            if (_despawner)
            {
                _despawner.OnNpcDespawned += DescreaseCounter;
            } 
        }

        private void OnDisable()
        {
            if (_spawner)
            {
                _spawner.OnNpcSpawned -= IncrementCounter;
            }
            if (_despawner)
            {
                _despawner.OnNpcDespawned -= DescreaseCounter;
            }
        }
    }
}
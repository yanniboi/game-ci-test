using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonKey : MonoBehaviour
{
    private Button _button;
    public bool IsShowing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsShowing && Input.GetKeyDown(KeyCode.Space))
        {
            _button.onClick.Invoke();
        }
    }
}

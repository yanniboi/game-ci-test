using System;
using UI;
using UnityEngine;

namespace NPC
{
    public class NpcIdentity : MonoBehaviour
    {

        public Identity Identity;
        public string Name = "Frank";
        public string Text = "Some text";

        private GameObject _highlighter;
        
        private void OnEnable()
        {
            Identity = new Identity(Name, Text);
            Identity.OnHighlight += Highlight;
            Identity.OnDehighlight += Dehighlight;
            
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child.name == "Selector")
                {
                    _highlighter = child;
                }
            }
        }

        private void OnDisable()
        {
            Identity.OnHighlight -= Highlight;
            Identity.OnDehighlight -= Dehighlight;
        }

        private void Highlight()
        {
            if (_highlighter)
            {
                _highlighter.SetActive(true);
            }
        }
        
        private void Dehighlight()
        {
            if (_highlighter)
            {
                _highlighter.SetActive(false);
            }
        }
    }
}

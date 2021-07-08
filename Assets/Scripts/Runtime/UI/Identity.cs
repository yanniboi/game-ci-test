using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Identity : Selectable, ICalloutData
    {
        private string _name;
        private string _text;
        private Sprite _image;
        private int _fontSize;

        public Identity(string name = null, string text = null, Sprite image = null, int fontSize = 50)
        {
            _name = name;
            _text = text;
            _image = image;
            _fontSize = fontSize;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetText()
        {
            return _text;
        }

        public Sprite GetImage()
        {
            return _image;
        }
        
        public int GetFontSize()
        {
            return _fontSize;
        }
    }
}
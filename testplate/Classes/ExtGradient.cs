using System;
using UnityEngine;

namespace SillyMenu.Classes
{
    public class ExtGradient
    {
        public GradientColorKey[] colors = new GradientColorKey[]
        {
            new GradientColorKey(new Color(0.996f,0.486f,0.890f), 0f),
            new GradientColorKey(new Color(0.996f,0.486f,0.890f), 0.5f),
            new GradientColorKey(new Color(0.996f,0.486f,0.890f), 1f)
        };

        public bool isRainbow = false;
        public bool copyRigColors = false;
    }
}

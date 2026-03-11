using System;
using System.Collections.Generic;
using System.Linq;
using AUIT.AdaptationObjectives.Definitions;
using UnityEngine;

namespace AUIT.Extras
{
    [Serializable]
    public class UIConfiguration
    {
        public Layout[] elements { get; set; }

        public UIConfiguration()
        {
            
        }

        public UIConfiguration(Layout[] layout)
        {
            elements = layout;
        }

        public static UIConfiguration FromLayout(Layout layout)
        {
            UIConfiguration config = new UIConfiguration();
            config.elements = new Layout[1];
            config.elements[0] = layout;
            return config;
        }
        
        public static UIConfiguration FromLayout(List<Layout> layouts)
        {
            UIConfiguration config = new UIConfiguration();
            config.elements = new Layout[layouts.Count];
            for (int i = 0; i < layouts.Count; i++)
            {
                config.elements[i] = layouts[i];
            }
            return config;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < elements.Count(); i++)
            {
                result += elements[i].ToString();
            }
            return result;
        }
    }
}
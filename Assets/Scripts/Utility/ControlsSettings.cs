using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [CreateAssetMenu(fileName = "Controls settings", menuName = "Controls settings")]
    public class ControlsSettings : ScriptableObject
    {
        [Header("Interaction")]
        public KeyCode Interact;
        public KeyCode Play;

        [Header("Movement")] 
        public KeyCode Run;
        public KeyCode Crawl;

        [Header("Menus")] 
        public KeyCode ItemsMenu;
        public KeyCode Menu;

    }
}
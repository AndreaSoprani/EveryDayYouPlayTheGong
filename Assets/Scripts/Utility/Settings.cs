using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings")]
    public class Settings : ScriptableObject
    {
        [Header("Keyboard Settings")] 
        public KeyCode Up;
        public KeyCode Right;
        public KeyCode Down;
        public KeyCode Left;
        
        public KeyCode Interact;
        public KeyCode Play;

        public KeyCode Run;
        public KeyCode Crawl;

        public KeyCode ItemsMenu;
        public KeyCode Menu;

        [Header("Audio Settings")] 
        
        [Range(0,100)] public int MusicVolume;
        [Range(0,100)] public int  SFXVolume;

        [Header("Game Settings")] 
        public int QuestsPerDay;

        [Header("Text displayed on startup")] 
        public TextAsset StartupText;

        public Dictionary<Vector3, KeyCode> GetDirectionsKeyCodes()
        {
            return new Dictionary<Vector3, KeyCode>()
            {
                {Vector3.up, Up},
                {Vector3.right, Right},
                {Vector3.down, Down},
                {Vector3.left, Left},
            };
        }

        [Header("GUI Settings")] 
        public float NotificationsDelayTime;


    }
}
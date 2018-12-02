using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings")]
    public class Settings : ScriptableObject
    {
        [Header("Keyboard Settings")]
        public KeyCode Interact;
        public KeyCode Play;

        public KeyCode Run;
        public KeyCode Crawl;

        public KeyCode ItemsMenu;
        public KeyCode Menu;

        [Header("Audio Settings")] 
        public bool MusicOn;
        public bool SFXOn;
        [Range(0,1)] public float MusicVolume;
        [Range(0,1)] public float SFXVolume;

        [Header("Game Settings")] 
        public int QuestsPerDay;


    }
}
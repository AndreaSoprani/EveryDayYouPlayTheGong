using UnityEngine;

namespace Objects
{
    public abstract class InGameObject : MonoBehaviour
    {

        public string ObjectID; // ID of the object.
        
        /// <summary>
        /// Used to check if the object can be interacted with.
        /// Default true, override to make object not interactable.
        /// </summary>
        /// <returns>true if the object can be interacted with, false otherwise</returns>
        public virtual bool IsInteractable()
        {
            return true;
        }

        /// <summary>
        /// Used to interact with the object.
        /// Without override it does nothing.
        /// </summary>
        public virtual void Interact(Player player){}
        
        /// <summary>
        /// Used to check if the object can be played.
        /// Default false, override to make object playable.
        /// </summary>
        /// <returns>true if the object can be played, false otherwise</returns>
        public virtual bool IsPlayable()
        {
            return false;
        }

        /// <summary>
        /// Used to play the object.
        /// Without override it does nothing.
        /// </summary>
        public virtual void Play(){}
        
    }
}
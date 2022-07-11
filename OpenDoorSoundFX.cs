using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorSoundFX : MonoBehaviour
{
    [SerializeField] SoundName openDoor;

   
   public void PlaySound()
    {
            if(openDoor != SoundName.None)
                EventSystem.CallPlaySoundEvent(openDoor);
        
    }
}

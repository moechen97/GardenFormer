using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SoundManager 
{
    public static void PlaySound()
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        DestoryItself destory = soundGameObject.AddComponent<DestoryItself>();
        audioSource.PlayOneShot(GameAssets.i.PlantSound);
    }
    
    public static void PlayGrassSound(int grassnumber)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = GameAssets.i.PlantSoundsMixer;
        DestoryItself destory = soundGameObject.AddComponent<DestoryItself>();
       
        audioSource.PlayOneShot(GameAssets.i.GrassNormalSound[grassnumber]);
    }
}

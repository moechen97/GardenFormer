using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    public AudioMixerGroup PlantSoundsMixer;
    public AudioMixerGroup otherSoundMixer;

    public AudioClip PlantSound;
    public AudioClip[] GrassNormalSound;
    public AudioClip[] FlowerNormalSound;

}

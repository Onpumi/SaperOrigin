using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAction
{
    private Dictionary<TypesAudio, AudioClip> _clips;


    public SoundAction()
    {
        _clips = new Dictionary<TypesAudio, AudioClip>();
    }

    public void AddClip(TypesAudio type, AudioClip clip)
    {
        _clips[type] = clip;
    }



    public AudioClip GetClip(TypesAudio type)
    {
        return _clips[type];
    }

    

}

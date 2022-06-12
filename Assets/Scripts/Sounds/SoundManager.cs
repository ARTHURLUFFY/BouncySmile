using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {


    public float volume;
    public Sound[] sounds;
    public static SoundManager instance;

    //private bool muted = false;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        foreach( Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;

            s.source.volume = s.volume*volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }

        
    }

    public void CalculateVolumes()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * volume;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.Log("el sonido :" + name + " no existe ");
            return;

        }
        //if(!s.source.isPlaying)
            s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("el sonido :" + name + " no existe ");
            return;

        }
        s.source.Stop();
    }

    public Sound GetSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
       
        return s;
    }

    public bool CheckPlaying(string name)
    {
        Sound s = GetSound(name);
        if (s != null)
        {
            if (s.source.isPlaying)
            {
                return s.source.isPlaying;
            }
            else
                return false;
        }
        else
        {
            Debug.Log("El Sonido"+ name  + "  No Existe" );
            return false;
        }
    }
    public void SetVolumen(string name, float v)
    {
        Sound s = GetSound(name);
        s.source.volume = v;
        s.volume = v;
    }
    public void SetPitch(string name, float p)
    {
        Sound s = GetSound(name);
        s.source.pitch = p;
        s.pitch = p;
    }


    public void Mute()
    {
        //muted = !muted;
        //AudioListener.volume = muted ? 0 : 1;

        
    }

    private void LateUpdate()
    {
        if (PlayerPrefs.GetString("mute", "mute") == "muted")
        {
            AudioListener.volume = 0;
        }
        else
            AudioListener.volume = 1;
    }
}

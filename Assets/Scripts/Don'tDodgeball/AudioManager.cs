using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public bool loop;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 0.5f;
        [Range(.1f, 3f)]
        public float pitch = 1f;

        [HideInInspector]
        public AudioSource source;
    }

    public Sound[] sounds;
    public static AudioManager instance;
    public bool muted;
    public bool overtime;

    public GameObject muteX;

    void Awake()
    {
        muted = false;
        //Make sure there is only 1 AudioManager in the Scene
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            //Add AudioSource for every sound in the array
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    private void Start()
    {
        //Background music plays on start
    }

    public void Play(string name)
    {
        bool found = false;
        //Look for desired sound in array
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                //Play desired sound if not muted
                if (!muted)
                {
                    sounds[i].source.Play();
                }
                else
                {
                    Debug.Log("Audio is muted");
                }
                found = true;
            }
        }

        if (!found)
        {
            //Tells if desired sound could not be found
            Debug.LogWarning("Sound: " + name + " could not be found (Play)");
        }
    }

    public void Stop(string name)
    {
        bool found = false;
        //Look for desired sound in array
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                //Play desired sound if not muted
                if (!muted)
                {
                    sounds[i].source.Stop();
                }
                else
                {
                    Debug.Log("Audio is muted");
                }
                found = true;
            }
        }

        if (!found)
        {
            Debug.LogWarning("Sound: " + name + " could not be found (Stop)");
        }
    }

    public void Pause(string name)
    {
        bool found = false;
        //Look for desired sound in array
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                //Play desired sound if not muted
                if (muted)
                {
                    sounds[i].source.Pause();
                }
                else
                {
                    Debug.Log("Audio is muted");
                }
                found = true;
            }
        }

        if (!found)
        {
            Debug.LogWarning("Sound: " + name + " could not be found (Pause)");
        }
    }

    //Sets function for mute
    public void Mute()
    {
        //If it is not muted, mute the game
        if (!muted)
        {
            muted = true;
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Pause("Main Menu BGM");
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (overtime)
                {
                    Pause("Overtime BGM");
                }
                else
                {
                    Pause("In Game BGM");
                }
            }
            //muteX.SetActive(true);
        }

        //If it is muted, unmute the game
        else
        {
            muted = false;
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Play("Main Menu BGM");
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (overtime)
                {
                    Play("Overtime BGM");
                }
                else
                {
                    Play("In Game BGM");
                }
            }
        }
    }

    public void ChangePitch(string name, float pitch)
    {
        bool found = false;
        //Look for desired sound in array
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                //Change pitch
                sounds[i].source.pitch = pitch;
                found = true;
            }
        }

        if (!found)
        {
            //Tells if desired sound could not be found
            Debug.LogWarning("Sound: " + name + " could not be found (Play)");
        }
    }

    public void SlideVolume(System.Single Slidervolume)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source.volume = Slidervolume;
        }
    }
}

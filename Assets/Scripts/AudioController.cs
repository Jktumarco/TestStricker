using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController i;

    private void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { 
            DestroyImmediate(gameObject);
        }
       
    }
    private void Start()
    {
        PlayMusic("mainMusic");
    }
    public void PlaySFX(string name, float volume = 1f, float pitch = 1f)
    {
        AudioClip audioClip = GameLoader.I.GetAudioByName(name);
        AudioSource source = CreateNewSource(audioClip.name);
        source.clip = audioClip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
        Destroy(source.gameObject, audioClip.length);
    }
    public void PlayMusic(string name, float volume = 1f)
    {
        AudioClip audioClip = GameLoader.I.GetAudioByName(name);
        AudioSource source = CreateNewSource(audioClip.name);
        source.clip = audioClip;
        source.volume = volume;
        source.loop = true;
        source.Play();
    }

    public AudioSource CreateNewSource(string _name)
    {
        AudioSource newSource = new GameObject(_name).AddComponent<AudioSource>();
        newSource.transform.SetParent(i.transform);
        return newSource;
    }
}

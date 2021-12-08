using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]

public enum AudioCategory
{
    MASTER,
    MUSIC,
    EFFECTS,
    DIALOGUE
}

[System.Serializable]
public class AudioElement
{
    public string name = "New Sound";
    [HideInInspector] public AudioSource source;
    public AudioClip clip;
    [Range(0,1)] public float volume = 1;
    [Range(0, 1)] public float pitch = 0.5f;
    public bool loop;
    public AudioCategory audioCategory;
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer mixer;
    public AudioMixerGroup masterGroup;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup effectsGroup;
    public AudioMixerGroup dialogueGroup;

    [Range(0, 1)] public float masterVolume = 0.5f;
    [Range(0, 1)] public float musicVolume = 0.5f;
    [Range(0, 1)] public float effectsVolume = 0.5f;
    [Range(0, 1)] public float dialogueVolume = 0.5f;
    [Space]
    public AudioElement[] audioElements;


    Dictionary<string, int> _indexCache = new Dictionary<string, int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            SetupAudioSources();
            CacheAudioElementsIndexes();
        }
        else
        {
            Destroy(this);
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetupAudioSources()
    {
        mixer.SetFloat("masterVolume", masterVolume);
        mixer.SetFloat("musicVolume", musicVolume);
        mixer.SetFloat("effectsVolume", effectsVolume);
        mixer.SetFloat("dialogueVolume", dialogueVolume);

        for (int i = 0; i < audioElements.Length; i++)
        {
            audioElements[i].source = gameObject.AddComponent<AudioSource>();
            audioElements[i].source.clip = audioElements[i].clip;
            audioElements[i].source.pitch = audioElements[i].pitch;
            audioElements[i].source.loop = audioElements[i].loop;
            audioElements[i].source.volume = audioElements[i].volume;

            switch (audioElements[i].audioCategory)
            {
                case AudioCategory.MUSIC:
                    audioElements[i].source.outputAudioMixerGroup = musicGroup;
                    break;
                case AudioCategory.EFFECTS:
                    audioElements[i].source.outputAudioMixerGroup = effectsGroup;
                    break;
                case AudioCategory.DIALOGUE:
                    audioElements[i].source.outputAudioMixerGroup = dialogueGroup;
                    break;
            }
        }
    }
    public void Play(string audioName)
    {
        audioElements[_indexCache[audioName]].source.Play();
    }
    public void Play(string audioName, float delay)
    {
        audioElements[_indexCache[audioName]].source.PlayDelayed(delay);
    }
    public void SetMasterVolume(float volume)
    {
        SetVolume(AudioCategory.MASTER, volume);
    }
    public void SetMusicVolume(float volume)
    {
        SetVolume(AudioCategory.MUSIC, volume);
    }
    public void SetEffectsVolume(float volume)
    {
        SetVolume(AudioCategory.EFFECTS, volume);
    }
    public void SetDialogueVolume(float volume)
    {
        SetVolume(AudioCategory.DIALOGUE, volume);
    }
    public void SetVolume(AudioCategory audioCategory, float volume)
    {
        switch (audioCategory)
        {
            case AudioCategory.MASTER:
                masterVolume = volume;
                break;
            case AudioCategory.MUSIC:
                musicVolume = volume;
                break;
            case AudioCategory.EFFECTS:
                effectsVolume = volume;
                break;
            case AudioCategory.DIALOGUE:
                dialogueVolume = volume;
                break;
        }
        mixer.SetFloat("masterVolume", masterVolume);
        mixer.SetFloat("musicVolume", musicVolume);
        mixer.SetFloat("effectsVolume", effectsVolume);
        mixer.SetFloat("dialogueVolume", dialogueVolume);
    }
    private void CacheAudioElementsIndexes()
    {
        for(int i=0; i<audioElements.Length; i++)
        {
            _indexCache.Add(audioElements[i].name, i);
        }
    }
   
}

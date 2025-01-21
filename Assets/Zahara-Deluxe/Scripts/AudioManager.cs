using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("AudioManager");
                instance = go.AddComponent<AudioManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private float previousGameVolume = 1f;
    private AudioSource[] gameAudioSources;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PauseGameAudio()
    {
        // Guardar todos los AudioSource activos en la escena
        gameAudioSources = FindObjectsOfType<AudioSource>();

        // Guardar el volumen actual y silenciar
        foreach (AudioSource source in gameAudioSources)
        {
            if (source.gameObject.CompareTag("MenuAudio"))
                continue;

            previousGameVolume = source.volume;
            source.volume = 0f;
        }
    }

    public void ResumeGameAudio()
    {
        if (gameAudioSources != null)
        {
            foreach (AudioSource source in gameAudioSources)
            {
                if (source == null || source.gameObject.CompareTag("MenuAudio"))
                    continue;

                source.volume = previousGameVolume;
            }
        }
    }
}
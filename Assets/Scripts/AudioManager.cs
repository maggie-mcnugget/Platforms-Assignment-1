using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioClip gameplayMusic;

    private void Awake()
    {
        // singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "MainMenu")
        {
            musicSource.Stop();
        }
        else
        {

            if (!musicSource.isPlaying)
            {
                musicSource.clip = gameplayMusic;
                musicSource.loop = true;
                musicSource.Play();
            }
        }
    }
}

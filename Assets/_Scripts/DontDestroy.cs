using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DontDestroy : MonoBehaviour
{
    private static DontDestroy instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);

            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}

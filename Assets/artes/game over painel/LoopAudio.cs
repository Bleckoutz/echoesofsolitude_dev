using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbientAudio : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; // Loop contínuo
        audioSource.playOnAwake = true; // Começa automático
    }

    private void Start()
    {
        audioSource.Play();
    }
}

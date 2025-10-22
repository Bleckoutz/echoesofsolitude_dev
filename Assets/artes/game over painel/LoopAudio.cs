using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbientAudio : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; // Loop cont�nuo
        audioSource.playOnAwake = true; // Come�a autom�tico
    }

    private void Start()
    {
        audioSource.Play();
    }
}

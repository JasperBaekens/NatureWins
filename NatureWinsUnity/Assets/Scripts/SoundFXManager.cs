using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    //Howto use audio

    //on top of document

    //[SerializeField] private AudioClip _thisAudio;
    //[SerializeField] private float _volumeAudio;

    //add to place where audio produced
    //SoundFXManager.Instance.PlaySoundFXClip(_thisAudio, transform, _volumeAudio);


    public static SoundFXManager Instance; //singleton

    [SerializeField] private AudioSource _soundFXObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn GAmeObject
        AudioSource audioSource = Instantiate(_soundFXObject, spawnTransform.position, Quaternion.identity);

        //assign AudioClip
        audioSource.clip = audioClip;

        //volume
        audioSource.volume = volume;

        //play sound
        audioSource.Play();

        //destroy after playing, so after duration of clip
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}

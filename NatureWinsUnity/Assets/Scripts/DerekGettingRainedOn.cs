using UnityEngine;

public class DerekGettingRainedOn : MonoBehaviour, IHaveWaterEffect
{
    private DerekStats _derekStats;
    public ParticleSystem ParticlesFillWater;


    private bool DoWaterSplooshy;


    //Audio
    private bool IsAudioPlayedRainOn = false;
    private float AudioLengthRainOn;
    private float AudioLengthRainOnCounter;
    [SerializeField] private AudioClip _rainingOnAudio;
    [SerializeField] private float _volumeAudioRainedOn;


    private void Awake()
    {
        _derekStats = FindAnyObjectByType<DerekStats>();
        AudioLengthRainOn = _rainingOnAudio.length;
    }
    private void Update()
    {
        if (DoWaterSplooshy)
        {
            DoParticleEffect(ParticlesFillWater);
            DoWaterSplooshy = false;
        }
        else
        {
            StopParticleEffect(ParticlesFillWater);
        }


    }

    public void DoWaterEffect()
    {
        _derekStats.DerekLoserMeter += 1f*Time.deltaTime;
        DoWaterSplooshy = true;


        if (!IsAudioPlayedRainOn && AudioLengthRainOnCounter > 0)
        {
            SoundFXManager.Instance.PlaySoundFXClip(_rainingOnAudio, transform, _volumeAudioRainedOn);
            IsAudioPlayedRainOn = true;
        }


        if (AudioLengthRainOnCounter >= AudioLengthRainOn)
        {
            AudioLengthRainOnCounter = 0;
            IsAudioPlayedRainOn = false;
        }

        AudioLengthRainOnCounter += Time.deltaTime;
    }


    private void DoParticleEffect(ParticleSystem pS)
    {
        ParticleSystem.EmissionModule emission = pS.emission;
        emission.rateOverTime = 30f;
    }
    private void StopParticleEffect(ParticleSystem pS)
    {
        ParticleSystem.EmissionModule emission = pS.emission;
        emission.rateOverTime = 0f;
    }

}

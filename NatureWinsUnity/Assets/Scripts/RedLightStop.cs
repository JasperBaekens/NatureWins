using UnityEngine;
using static DerekStats;

public class RedLightStop : MonoBehaviour, IHaveDerekEffect
{
    DerekStats _derekStats;
    public bool BeenUsed = false;

    [SerializeField] private AudioClip _thisAudio;
    [SerializeField] private float _volumeAudio;

    private float WaitTimerCurrent = 0;
    private float WaitTimerNeeded = 1;
    private int WaitTimerRepeats = 3;
    private int WaitTimerCounter = 0;

    private void Awake()
    {
        _derekStats = FindAnyObjectByType<DerekStats>();
        SoundFXManager.Instance.PlaySoundFXClip(_thisAudio, transform, _volumeAudio);
        WaitTimerCounter++;
    }
    private void Update()
    {
        WaitTimerCurrent += Time.deltaTime;

        if (WaitTimerCurrent >= WaitTimerNeeded && WaitTimerCounter <= WaitTimerRepeats && BeenUsed)
        {
            SoundFXManager.Instance.PlaySoundFXClip(_thisAudio, transform, _volumeAudio);
            WaitTimerCounter++;
            WaitTimerCurrent = 0;
        }
    }
    public void EffectOnDerek()
    {
        if (!BeenUsed)
        {
            _derekStats.DerekLoserMeter += 5;
            _derekStats.DerekCurrentMode = DerekModeStates.Standing;
            BeenUsed = true;
        }
    }

}

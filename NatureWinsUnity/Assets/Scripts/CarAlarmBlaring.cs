using UnityEngine;
using static DerekStats;

public class CarAlarmBlaring : MonoBehaviour, IHaveDerekEffect
{
    [SerializeField] private AudioClip _carAlarmAudio;
    [SerializeField] private float _volumeAudio;

    DerekStats _derekStats;
    public bool BeenUsed = false;

    private float _carAlarmTimeLimit = 2f;
    private float _carAlarmTimeCurrent = 0f;

    [SerializeField] private GameObject AfterActivation;



    private void Awake()
    {
        _derekStats = FindAnyObjectByType<DerekStats>();
        SoundFXManager.Instance.PlaySoundFXClip(_carAlarmAudio, transform, _volumeAudio);
    }

    private void Update()
    {
        _carAlarmTimeCurrent += Time.deltaTime;
        if (_carAlarmTimeCurrent >= _carAlarmTimeLimit)
        {
            GameObject spawnedObject = Instantiate(AfterActivation);
            spawnedObject.transform.position = transform.position;
            GameObject.Destroy(gameObject);
        }
    }
    public void EffectOnDerek()
    {
        if (!BeenUsed)
        {
            _derekStats.DerekLoserMeter += 5;
            _derekStats.DerekCurrentMode = DerekModeStates.Suprise;
            BeenUsed = true;
        }
    }
}

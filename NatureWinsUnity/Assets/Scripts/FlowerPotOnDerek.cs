using UnityEngine;

public class FlowerPotOnDerek : MonoBehaviour, IHaveDerekEffect
{
    DerekStats _derekStats;
    public bool BeenUsed = false;
    private Transform _previousTransform;
    private bool _firstframe = true;
    public bool FellOnFoor = false;



    [SerializeField] private AudioClip _flowerPotAudio;
    [SerializeField] private float _volumeAudio;


    private void Awake()
    {
        _derekStats = FindAnyObjectByType<DerekStats>();
        _previousTransform = transform;
    }

    private void Update()
    {
        if (transform.position.y == _previousTransform.position.y && !_firstframe && !FellOnFoor)
        {
            FellOnFoor = true;
            SoundFXManager.Instance.PlaySoundFXClip(_flowerPotAudio, transform, _volumeAudio);

        }
        _previousTransform = transform;

        _firstframe = false;
    }

    public void EffectOnDerek()
    {
        Debug.Log("FlowerPot Hit Derek");
        if (!BeenUsed)
        {
            _derekStats.DerekLoserMeter += 10;
            BeenUsed = true;
        }
    }
}

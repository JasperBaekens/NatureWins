using UnityEngine;

public class SupplyWater : MonoBehaviour, IGiveWater
{
    private CloudStats _cloudStats;

    [SerializeField] private AudioClip WaterFillAudio;
    [SerializeField] private float _volumeAudio;



    private void Awake()
    {
        _cloudStats = FindAnyObjectByType<CloudStats>();
    }
    void IGiveWater.GiveWater()
    {
        SoundFXManager.Instance.PlaySoundFXClip(WaterFillAudio, transform, _volumeAudio);

        Debug.Log($"{gameObject.name} got hit. Will give 1 WaterSupply");
        _cloudStats.WaterSupply += 1;
    }
}

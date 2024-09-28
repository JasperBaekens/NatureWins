using UnityEngine;

public class DerekGettingRainedOn : MonoBehaviour, IHaveWaterEffect
{
    private DerekStats _derekStats;
    private void Awake()
    {
        _derekStats = FindAnyObjectByType<DerekStats>();
    }

    public void DoWaterEffect()
    {
        _derekStats.DerekLoserMeter += 1f*Time.deltaTime;
    }
}

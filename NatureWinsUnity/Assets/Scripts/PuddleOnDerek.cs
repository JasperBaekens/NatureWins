using UnityEngine;

public class PuddleOnDerek : MonoBehaviour, IHaveDerekEffect
{
    DerekStats _derekStats;
    public bool BeenUsed = false;


    private void Awake()
    {
        _derekStats = FindAnyObjectByType<DerekStats>();
    }
    public void EffectOnDerek()
    {
        if (!BeenUsed)
        {
            _derekStats.DerekLoserMeter += 5;
            BeenUsed = true;
        }
    }
}

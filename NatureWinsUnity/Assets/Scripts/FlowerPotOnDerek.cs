using UnityEngine;

public class FlowerPotOnDerek : MonoBehaviour, IHaveDerekEffect
{
    DerekStats _derekStats;
    public bool BeenUsed = false;


    private void Awake()
    {
        _derekStats = FindAnyObjectByType<DerekStats>();
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

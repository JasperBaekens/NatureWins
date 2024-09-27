using UnityEngine;

public class SupplyWater : MonoBehaviour, IGiveWater
{
    private CloudStats _cloudStats;

    private void Awake()
    {
        _cloudStats = FindAnyObjectByType<CloudStats>();
    }
    void IGiveWater.GiveWater()
    {
        Debug.Log($"{gameObject.name} got hit. Will give 1 WaterSupply");
        _cloudStats.WaterSupply += 1;
    }
}

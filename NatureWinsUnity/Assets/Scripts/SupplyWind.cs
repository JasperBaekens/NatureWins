using UnityEngine;

public class SupplyWind : MonoBehaviour, IGiveWind
{
    private CloudStats _cloudStats;

    private void Awake()
    {
        _cloudStats = FindAnyObjectByType<CloudStats>();
    }

    public void GiveWind()
    {
        Debug.Log($"{gameObject.name} got hit. Will give 1 WindSupply");
        _cloudStats.WindSupply += 1;
    }
}

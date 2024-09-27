using UnityEngine;

public class SupplyElek : MonoBehaviour, IGiveElek
{
    private CloudStats _cloudStats;

    public void GiveElek()
    {
        Debug.Log($"{gameObject.name} got hit. Will give 1 ElekSupply");
        _cloudStats.ElekSupply += 1;
    }

    private void Awake()
    {
        _cloudStats = FindAnyObjectByType<CloudStats>();
    }
}

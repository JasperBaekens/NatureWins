using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class DerekStats : MonoBehaviour
{
    [Range(0f, 100f)] public float DerekLoserMeter = 0f;
    public float DerekLoserLookORadius = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, DerekLoserLookORadius);
        foreach (var hitCollider in hitColliders)
        {
            ActivateDerekEffect(hitCollider.gameObject);
        }
    }

    private void ActivateDerekEffect(GameObject gameObject)
    {
        IHaveDerekEffect[] derekEffectHavers = gameObject.GetComponents<IHaveDerekEffect>();
        foreach (IHaveDerekEffect derekEffectHaver in derekEffectHavers)
        {
            derekEffectHaver.EffectOnDerek();
        }
    }
}

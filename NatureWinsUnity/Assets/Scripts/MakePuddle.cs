using UnityEngine;

public class MakePuddle : MonoBehaviour, IHaveWaterEffect
{
    [SerializeField] private GameObject AfterActivation;
    public float NeededForActivationCounterLimit = 1;
    public float NeededForActivationCounter = 0;
    public ParticleSystem ParticlesFillWater;

    private bool DoWaterSplooshy;

    private void Update()
    {
        if (DoWaterSplooshy)
        {
            DoParticleEffect(ParticlesFillWater);
            DoWaterSplooshy = false;
        }
        else
        {
            StopParticleEffect(ParticlesFillWater);
        }

    }

    public void DoWaterEffect()
    {

        if (NeededForActivationCounter < NeededForActivationCounterLimit)
        {
            DoWaterSplooshy = true;
            NeededForActivationCounter += Time.deltaTime;
            if (NeededForActivationCounter >= NeededForActivationCounterLimit)
            {
                GameObject spawnedObject = Instantiate(AfterActivation);
                spawnedObject.transform.position = transform.position;
                gameObject.SetActive(false);
            }
        }
    }
    private void DoParticleEffect(ParticleSystem pS)
    {
        ParticleSystem.EmissionModule emission = pS.emission;
        emission.rateOverTime = 30f;
    }
    private void StopParticleEffect(ParticleSystem pS)
    {
        ParticleSystem.EmissionModule emission = pS.emission;
        emission.rateOverTime = 0f;
    }


}

using UnityEngine;

public class MakeCarAlarmGoOff : MonoBehaviour, IHaveElekEffect
{
    [SerializeField] private GameObject AfterActivation;
    public float NeededForActivationCounterLimit = 1;
    public float NeededForActivationCounter = 0;
    public ParticleSystem ParticlesFillElek;

    private bool DoElekSplooshy;

    private void Update()
    {
        if (DoElekSplooshy)
        {
            DoParticleEffect(ParticlesFillElek);
            DoElekSplooshy = false;
        }
        else
        {
            StopParticleEffect(ParticlesFillElek);
        }

    }

    public void DoElekEffect()
    {

        if (NeededForActivationCounter < NeededForActivationCounterLimit)
        {
            DoElekSplooshy = true;
            NeededForActivationCounter += Time.deltaTime;
            if (NeededForActivationCounter >= NeededForActivationCounterLimit)
            {
                GameObject spawnedObject = Instantiate(AfterActivation);
                spawnedObject.transform.position = transform.position;
                GameObject.Destroy(gameObject);
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

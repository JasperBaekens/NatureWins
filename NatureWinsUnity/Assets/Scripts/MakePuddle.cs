using UnityEngine;

public class MakePuddle : MonoBehaviour, IHaveWaterEffect
{
    [SerializeField] private GameObject AfterActivation;
    public float NeededForActivationCounterLimit = 1;
    public float NeededForActivationCounter = 0;


    public void DoWaterEffect()
    {
        if (NeededForActivationCounter < NeededForActivationCounterLimit)
        {
            NeededForActivationCounter += Time.deltaTime;
            if (NeededForActivationCounter >= NeededForActivationCounterLimit)
            {
                GameObject spawnedObject = Instantiate(AfterActivation);
                spawnedObject.transform.position = transform.position;
                gameObject.SetActive(false);
            }
        }
    }

}

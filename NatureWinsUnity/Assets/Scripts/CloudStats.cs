using UnityEngine;

public class CloudStats : MonoBehaviour
{
    //values of the different Current Amount of Resources
    [Range(0f, 20f)] public float WaterSupply = 5f;
    [Range(0f, 20f)] public float ElekSupply = 5f;
    [Range(0f, 20f)] public float WindSupply = 5f;

    public enum ElementMode
    {
        Water,
        Elek,
    }

    public ElementMode CurrentMode;

    [SerializeField] Material materialWaterMode;
    [SerializeField] Material materialElekMode;
    [SerializeField] GameObject GameObject;

    // Update is called once per frame
    void Update()
    {
        switch (CurrentMode)
        {
            case ElementMode.Water:
                if (GameObject.GetComponent<Renderer>().material != materialWaterMode)
                {
                    GameObject.GetComponent<Renderer>().material = materialWaterMode;
                }
                break;

            case ElementMode.Elek:
                if (GameObject.GetComponent<Renderer>().material != materialElekMode)
                {
                    GameObject.GetComponent<Renderer>().material = materialElekMode;
                }
                break;
        }

    }

}

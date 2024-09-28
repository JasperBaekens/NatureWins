using UnityEngine;
using UnityEngine.UI;

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


}

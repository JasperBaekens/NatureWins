using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image DerekLoserBar;
    public Image WaterBar;


    private CloudStats _cloudStats;
    private DerekStats _derekStats;


    private void Awake()
    {
        _cloudStats = FindAnyObjectByType<CloudStats>();
        _derekStats = FindAnyObjectByType<DerekStats>();
    }



    void Update()
    {
        //updates motivationbar visual on ui
        DerekLoserBar.fillAmount = _derekStats.DerekLoserMeter / 100; // DerekLoserMeter Max
        WaterBar.fillAmount = _cloudStats.WaterSupply / 20; //watersupplymax
    }
}

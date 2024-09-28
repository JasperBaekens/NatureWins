using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image DerekLoserBar;
    public Image WaterBar;
    public Image ElekBar;



    private CloudStats _cloudStats;
    private DerekStats _derekStats;

    [SerializeField] public Image _endScreen1;
    [SerializeField] public Image _endScreen2;
    [SerializeField] public Image _endScreen3;
    [SerializeField] public Image _whiteScreen;


    private void Awake()
    {
        _cloudStats = FindAnyObjectByType<CloudStats>();
        _derekStats = FindAnyObjectByType<DerekStats>();

        _endScreen1.enabled = false;
        _endScreen2.enabled = false;
        _endScreen3.enabled = false;
        _whiteScreen.enabled = false;
    }



    void Update()
    {
        //updates motivationbar visual on ui
        DerekLoserBar.fillAmount = _derekStats.DerekLoserMeter / 100; // DerekLoserMeter Max
        if (WaterBar.enabled)
        {
            WaterBar.fillAmount = _cloudStats.WaterSupply / 20; //watersupplymax
        }
        if (ElekBar.enabled)
        {
            ElekBar.fillAmount = _cloudStats.ElekSupply / 20;
        }
    }
}

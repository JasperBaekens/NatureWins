using UnityEngine;

public class FinishLine : MonoBehaviour, IHaveDerekEffect
{
    private DerekStats _derekStats;
    private UIManager _uiManager;

    private void Awake()
    {
        _derekStats = FindAnyObjectByType<DerekStats>();
        _uiManager = FindAnyObjectByType<UIManager>();
    }

    public void EffectOnDerek()
    {
        if (_derekStats.DerekLoserMeter <= 33f)
        {
            _uiManager._endScreen1.enabled = true;
        }
        if (_derekStats.DerekLoserMeter <= 66f && _derekStats.DerekLoserMeter >= 33f)
        {
            _uiManager._endScreen2.enabled = true;
        }
        if (_derekStats.DerekLoserMeter >= 66f)
        {
            _uiManager._endScreen3.enabled = true;
        }
    }
}

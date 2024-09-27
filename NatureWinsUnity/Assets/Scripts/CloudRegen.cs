using UnityEngine;

public class CloudRegen : MonoBehaviour
{
    private Camera _camera;
    private RaycastHit previousHitInfo;

    [SerializeField] private GameObject _playerModel;

    private void Awake()
    {
        _camera = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(_camera.transform.position, (transform.position - _camera.transform.position).normalized);

        if (Physics.SphereCast(_camera.transform.position, _playerModel.transform.localScale.x, (transform.position - _camera.transform.position).normalized, out RaycastHit hitInfo, _camera.farClipPlane))
        {
            if (hitInfo.collider != null && previousHitInfo.collider != hitInfo.collider)
            {
                //Debug.Log($"{hitInfo.collider.gameObject.name} got clicked on.");
                GiveWater(hitInfo.collider.gameObject);
                GiveWind(hitInfo.collider.gameObject);
                GiveElek(hitInfo.collider.gameObject);

            }
        }
        previousHitInfo = hitInfo;
    }
    void GiveWater(GameObject gameObject)
    {
        IGiveWater[] waterGivers = gameObject.GetComponents<IGiveWater>();
        foreach (IGiveWater waterGiver in waterGivers)
        {
            waterGiver.GiveWater();
        }
    }
    void GiveWind(GameObject gameObject)
    {
        IGiveWind[] windGivers = gameObject.GetComponents<IGiveWind>();
        foreach (IGiveWind windGiver in windGivers)
        {
            windGiver.GiveWind();
        }
    }
    void GiveElek(GameObject gameObject)
    {
        IGiveElek[] elekGivers = gameObject.GetComponents<IGiveElek>();
        foreach (IGiveElek elekGiver in elekGivers)
        {
            elekGiver.GiveElek();
        }
    }

}

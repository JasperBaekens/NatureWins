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
        Debug.DrawRay(_camera.transform.position, (transform.position - _camera.transform.position).normalized);

        if (Physics.SphereCast(_camera.transform.position, _playerModel.transform.localScale.x, (transform.position - _camera.transform.position).normalized, out RaycastHit hitInfo, _camera.farClipPlane))
        {
            if (hitInfo.collider != null && previousHitInfo.collider != hitInfo.collider)
            {
                //Debug.Log($"{hitInfo.collider.gameObject.name} got clicked on.");
                GiveWater(hitInfo.collider.gameObject);
            }
            previousHitInfo = hitInfo;
        }
    }
    void GiveWater(GameObject gameObject)
    {
        //single clickable behaviour
        //IClickable clickable = gameObject.GetComponent<IClickable>(); //make a variable that can only hold the IClickable gameobjects that have the IClickable interface
        //clickable?.HandleClick(); //if clickable object is not null then execute handleclick

        //mutliple clickable behaviours
        IGiveWater[] watergivers = gameObject.GetComponents<IGiveWater>();
        foreach (IGiveWater watergiver in watergivers)
        {
            watergiver.GiveWater();
        }
    }

}

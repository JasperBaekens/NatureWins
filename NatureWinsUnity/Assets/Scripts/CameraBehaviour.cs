using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _objectToFollow;

    private Vector3 _objectToFollowStartPos; //transform of camera object in the scene

    private void Awake()
    {
        _objectToFollowStartPos = _objectToFollow.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = _objectToFollow.transform.position - _objectToFollowStartPos;
    }
}

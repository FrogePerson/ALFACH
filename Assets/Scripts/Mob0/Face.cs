using UnityEngine;
using Mirror;

public class Face : NetworkBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _face;
    private Move _Move;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float maxRotationX = 45f;
    [SerializeField] private float minRotationX = -45f;

    [SerializeField] private Vector3 Kall;

    [SerializeField] private float rotx;

    void Start()
    {
        _Move = GetComponent<Move>();
    }

    private void RoteFace()
    {
        
        //Kall = _face.transform.position;
        //Vector3 directionToTarget = _target.position + _face.transform.position;

        //Quaternion targetRotation = Quaternion.LookRotation(directionToTarget.normalized);
        //float targetRotationX = targetRotation.eulerAngles.x;

        //targetRotationX = Mathf.Clamp(targetRotationX, minRotationX, maxRotationX);

        //Quaternion limitedRotation = Quaternion.Euler(targetRotationX, _face.transform.eulerAngles.y, _face.transform.eulerAngles.z);

        //_face.transform.rotation = limitedRotation;
        _face.transform.LookAt(_target.transform);
        if(_Move._IsJump == 0)
        {
            ClampRotation();
            rotx = _face.transform.rotation.x;
        }
        
    }

    private void ClampRotation()
    {
        if((_face.transform.eulerAngles.x > minRotationX) && (_face.transform.eulerAngles.x < 180))
        {
            _face.transform.eulerAngles = new Vector3(minRotationX,_face.transform.rotation.y,_face.transform.rotation.z);
        }
        if((_face.transform.eulerAngles.x < maxRotationX) && (_face.transform.eulerAngles.x > 180))
        {
            _face.transform.eulerAngles = new Vector3(maxRotationX,_face.transform.rotation.y,_face.transform.rotation.z);
        }
    }

    void Update()
    {
        RoteFace();
    }
}

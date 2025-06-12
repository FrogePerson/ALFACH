using UnityEngine;
using Mirror;

public class ShootEffect : NetworkBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private float _dieTime;

    private void OnEnable()
    {
        _time=Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - _time) > _dieTime)
        {
           gameObject.SetActive(false);
        }
    }
}

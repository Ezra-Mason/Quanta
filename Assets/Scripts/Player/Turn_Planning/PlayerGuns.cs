using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector2 direction)
    {
        Vector3 point = transform.position + new Vector3(direction.x, transform.position.y, direction.y);
        //need to sort out bullet facing
        GameObject instance = Instantiate(_bulletPrefab, point, Quaternion.identity);
        if (instance.TryGetComponent<Bullet>(out Bullet bullet))
        {
            bullet.Spawn(direction);
        }
        
    }
}

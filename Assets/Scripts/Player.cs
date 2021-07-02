using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ObjectPooler.Instance.SpawnFromPool("Bullet", _shootPoint.position, quaternion.identity);
            ObjectPooler.Instance.SpawnFromPool("Bullet", _shootPoint.position + new Vector3(0, 0.5f, 0),
                quaternion.identity);
            ObjectPooler.Instance.SpawnFromPool("Bullet", _shootPoint.position + new Vector3(0, -0.5f, 0),
                quaternion.identity);
        }
    }
}

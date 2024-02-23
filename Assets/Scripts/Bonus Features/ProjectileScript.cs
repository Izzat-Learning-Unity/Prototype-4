using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float spawnTime = 0;
    public GameObject enemyToFollow;

    private Rigidbody projectileRB;
    void Start()
    {
        spawnTime = Time.time;
        projectileRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTime >= 3)
        {
            Destroy(gameObject);
        }

        if(enemyToFollow != null)
        {
            Rigidbody projectileRB = GetComponent<Rigidbody>();
            Vector3 towardsEnemy = enemyToFollow.transform.position - transform.position;
            projectileRB.AddForce(towardsEnemy * 30, ForceMode.Force);
        }
        


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("Enemy"))
        {
            enemyToFollow = null;
            projectileRB.useGravity = true;
        }
    }
}

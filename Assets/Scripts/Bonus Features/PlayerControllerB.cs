using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerB : MonoBehaviour
{
    private float speed = 2f;
    private float powerUpStrength = 20;
    private Rigidbody playerRB;
    private GameObject focalPoint;

    public GameObject powerUpIndicator1;
    public GameObject powerUpIndicator2;
    public GameObject powerUpIndicator3;
    public GameObject projectilePrefab;
    public Transform projectileOrigin;

    private bool hasPowerup1 = false;
    private bool hasPowerup2 = false;
    private bool hasPowerup3 = false;

    private float verticalInput;

    private float timePassed = 0;
    private float timeToWait = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 10 && !hasPowerup3)
        {

            playerRB.velocity = Vector3.zero;
            playerRB.AddForce(Vector3.down * 120, ForceMode.Impulse);
        }
        verticalInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * speed * verticalInput);
        powerUpIndicator1.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        powerUpIndicator2.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        powerUpIndicator3.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (hasPowerup2)
        {
            if (timePassed >= timeToWait)
            {
                shootProjectiles();
                timePassed = 0;
            }
            else
            {
                timePassed += Time.deltaTime;
            }

        }

        if (hasPowerup3)
        {
            if (timePassed >= timeToWait - 0.2f)
            {
                jumpUpAndSmash();
                timePassed = 0;
            }
            else
            {
                timePassed += Time.deltaTime;
            }

        }
    }

    private void jumpUpAndSmash()
    {



        if (transform.position.y > 10)
        {

            playerRB.velocity = Vector3.zero;
            playerRB.AddForce(Vector3.down * 120, ForceMode.Impulse);
        }
        else
        {
            playerRB.AddForce(Vector3.up * 100, ForceMode.Impulse);
        }




    }

    private void shootProjectiles()
    {
        GameObject[] spawnedEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < spawnedEnemies.Length; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileOrigin);
            projectile.GetComponent<ProjectileScript>().enemyToFollow = spawnedEnemies[i];

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            if (other.name == "Powerup 1")
            {
                hasPowerup1 = true;
                Destroy(other.gameObject);
                StartCoroutine(PowerupCountdownRoutine1());
                powerUpIndicator1.SetActive(true);
            }
            else

                if (other.name == "Powerup 2")
            {
                hasPowerup2 = true;
                Destroy(other.gameObject);
                StartCoroutine(PowerupCountdownRoutine2());
                powerUpIndicator2.SetActive(true);
            }
            else
                 if (other.name == "Powerup 3")
            {
                jumpUpAndSmash();
                hasPowerup3 = true;
                Destroy(other.gameObject);
                StartCoroutine(PowerupCountdownRoutine3());
                powerUpIndicator3.SetActive(true);
            }


        }
    }

    IEnumerator PowerupCountdownRoutine1()
    {
        yield return new WaitForSeconds(7);
        hasPowerup1 = false;
        powerUpIndicator1.SetActive(false);

    }

    IEnumerator PowerupCountdownRoutine2()
    {
        yield return new WaitForSeconds(3);
        hasPowerup2 = false;
        powerUpIndicator2.SetActive(false);

    }

    IEnumerator PowerupCountdownRoutine3()
    {
        yield return new WaitForSeconds(3);
        hasPowerup3 = false;
        powerUpIndicator3.SetActive(false);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup1)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;


            enemyRB.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);

        }

        if (collision.gameObject.CompareTag("Ground") && hasPowerup3)
        {

            GameObject[] spawnedEnemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < spawnedEnemies.Length; i++)
            {
                Rigidbody enemyRB = spawnedEnemies[i].GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = ((spawnedEnemies[i].transform.position - collision.GetContact(0).point).normalized);

                awayFromPlayer = new Vector3(1 / awayFromPlayer.x, 1 / awayFromPlayer.y, 1 / awayFromPlayer.z);

                enemyRB.AddForce(awayFromPlayer * 5, ForceMode.Impulse);
            }
        }
    }
}

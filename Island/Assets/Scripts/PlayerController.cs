using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private float speed = 25;
    GameObject focalPoint;

    //powerup values

    bool hasPowerup;
    public float powerupStrength = 15;
    GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

        //powerup
        powerupIndicator = GameObject.Find("PowerupIndicator");
        powerupIndicator.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput * Time.deltaTime, ForceMode.Force);

        //powerUp
        powerupIndicator.transform.position = transform.position;
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
            powerupIndicator.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 pushAwayEnemy = (other.gameObject.transform.position - transform.position);
            Debug.Log("We've just hitted " + other.gameObject + " with the " + hasPowerup + " powerup!");
            enemyRB.AddForce(pushAwayEnemy * powerupStrength, ForceMode.Impulse);
        }
    }
}

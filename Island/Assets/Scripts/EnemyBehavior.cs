using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody enemyRB;
    public float speed;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirections = (player.transform.position - this.transform.position).normalized;
        enemyRB.AddForceAtPosition(lookDirections * speed * Time.deltaTime, player.transform.position, ForceMode.Impulse);

        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }
}

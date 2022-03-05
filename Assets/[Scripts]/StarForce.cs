using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarForce : MonoBehaviour, PooledStars
{
    public float upForce;
    public float sideForce;

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce, upForce);

        Vector2 force = new Vector2(xForce, yForce);

        GetComponent<Rigidbody2D>().velocity = force;
    }
}

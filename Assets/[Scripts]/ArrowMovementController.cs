using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovementController : MonoBehaviour
{
    public float maxY = 4.3f;
    public float minY = 3.7f;

    bool movingForward = true;

    float elapsedTime;
    public float duration = 2f;

    public void Update()
    {
        elapsedTime += Time.deltaTime;
        if (movingForward)
        {
            transform.localPosition = new Vector2 (transform.localPosition.x, Mathf.Lerp(maxY, minY, elapsedTime / duration));
            if (elapsedTime >= duration)
            {
                elapsedTime = 0f;
                movingForward = false;
            }
        }
        else
        {
            transform.localPosition = new Vector2 (transform.localPosition.x, Mathf.Lerp(minY, maxY, elapsedTime / duration));
            if (elapsedTime >= duration)
            {
                elapsedTime = 0f;
                movingForward = true;
            }
        }

    }
}

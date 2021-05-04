using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime;
    public Vector3 Offset;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        Offset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));

        transform.localPosition += Offset;
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(Vector2.up, 180);
    }
}


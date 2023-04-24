using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float velocity;

    private Vector2 targetPostion;

    float GetDistanceFromTarget()
    {
        return (new Vector2(this.transform.position.x, this.transform.position.z) - targetPostion).magnitude;
    }

    void Start()
    {
        targetPostion = new Vector2(transform.position.x, transform.position.z);
    }

    public void SetTraget(Vector2 target)
    {
        targetPostion = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetDistanceFromTarget() >= 3.0f)
        {
            //Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            Vector2 speed = MapManager.Instance.GetSpeed(transform.position.x, transform.position.z);

            GetComponent<Rigidbody>().velocity = new Vector3(speed.x, 0.0f, speed.y) * velocity;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    Rigidbody Ball;
    Vector3 mouseUpPos;
    Vector3 mouseDownPos;
    private float MAX_FORCE = 800.0f;
    private float mouseDownTimeStart;
    private float mouseDownTime;
    // Start is called before the first frame update
    void Start()
    {
        Ball = GetComponent<Rigidbody>();
    }

    public void apllyForce(Vector3 direction, float force)
    {
        Ball.AddForce((direction * force));
    }

    private float calculateForce(float Time)
    {
        float maxHoldTime = 2.0f;
        float normalizedHoldTime = Mathf.Clamp01(Time / maxHoldTime);
        float force = normalizedHoldTime * MAX_FORCE;

        return force;
    }
    // Update is called once per frame
    void Update()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100) ;
            mouseDownTimeStart = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDownTime = Time.time - mouseDownTimeStart;
            mouseUpPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            apllyForce((mouseDownPos - mouseUpPos).normalized,calculateForce(mouseDownTime));
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallBehavior : MonoBehaviour
{
    Rigidbody Ball;
    Vector3 mouseUpPos;
    Vector3 mouseDownPos;
    private float MAX_FORCE = 800.0f;
    private float mouseDownTimeStart = 0;
    private float mouseDownTime = 0;
    [SerializeField] Image forceBar;
    // Start is called before the first frame update
    void Start()
    {
        Ball = GetComponent<Rigidbody>();
        hideForce();
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
        if (Ball.freezeRotation)
            Ball.freezeRotation = false;


        mouseDownTime = Time.time - mouseDownTimeStart;
       
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100) ;
            mouseDownTimeStart = Time.time;

        }

        if(Input.GetMouseButton(0))
         showForce(calculateForce(mouseDownTime));

        if (Input.GetMouseButtonUp(0))
        {
            mouseUpPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            apllyForce((mouseDownPos - mouseUpPos).normalized,calculateForce(mouseDownTime));

            hideForce();
        }
       
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Goal Net")
        {
            Ball.transform.position = new Vector3(0.0f, 1.0f, -7.2f);
            Ball.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            Ball.freezeRotation = true;
 
            Debug.Log("GOAL");
        }

        if (collision.gameObject.tag == "OutsideNet")
        {
            Ball.transform.position = new Vector3(0.0f, 1.0f, -7.2f);
            Ball.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            Ball.freezeRotation = true;
            Debug.Log("OUT");
        }
    }
    void showForce(float force)
    {
        forceBar.fillAmount = force / MAX_FORCE ;
    }
    void hideForce()
    {
        forceBar.fillAmount = 0;
    }
}

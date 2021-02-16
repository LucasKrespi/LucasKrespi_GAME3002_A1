using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallBehavior : MonoBehaviour
{
    Rigidbody Ball;
    public float gravity;
    Vector3 mouseUpPos;
    Vector3 mouseDownPos;
    Vector3 velocity;
    public int goals;
    public bool hasScored;
    public int defenses;
    public bool hasKicked = false;
    private float MAX_FORCE = 25.0f;
    private float mouseDownTimeStart = 0;
    private float mouseDownTime = 0;
    Vector3 initialPos;
    [SerializeField] Image forceBar;
    [SerializeField] GameObject shadow;
    // Start is called before the first frame update
    void Start()
    {
        gravity = -9.81f;
        hasScored = false;
        defenses = 0;
        goals = 0;
        Ball = GetComponent<Rigidbody>();
        Ball.useGravity = false;
        hideForce();
        StartCoroutine(initicalPosition());
    }

    public void apllyForce(Vector3 direction, float lvelocity)
    {
        Ball.velocity = (direction * lvelocity);
        hasKicked = true;
    }

    private float calculateVelocity(float Time)
    {
        float maxHoldTime = 2.0f;
        float normalizedHoldTime = Mathf.Clamp01(Time / maxHoldTime);
        float velocity = normalizedHoldTime * MAX_FORCE;

        return velocity;
    }
    private Vector3 CalculateNormalizedDirection()
    {
        Vector3 temp;
        temp = (mouseDownPos - mouseUpPos).normalized;

        return temp;
    }
    // Update is called once per frame
    void Update()
    {
        if (Ball.freezeRotation)
            Ball.freezeRotation = false;


        mouseDownTime = Time.time - mouseDownTimeStart;
       
        if (Input.GetMouseButtonDown(0) && !hasKicked)
        {
            mouseDownPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100) ;
            mouseDownTimeStart = Time.time;

        }

        if (Input.GetMouseButton(0) && !hasKicked)
        {
            
            showForce(calculateVelocity(mouseDownTime));
        }

        if (Input.GetMouseButtonUp(0) && !hasKicked)
        {
            mouseUpPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            velocity = CalculateNormalizedDirection() * calculateVelocity(mouseDownTime);
            apllyForce(CalculateNormalizedDirection(), calculateVelocity(mouseDownTime));

            hideForce();

            StartCoroutine(resetGame());
        }
       
        
    }
    private void FixedUpdate()
    {
        Ball.AddForce(gravity * new Vector3(0.0f,1.0f,0.0f), ForceMode.Acceleration);
    }

    public Vector3 distanceTraveled()
    {
        Vector3 temp;

        temp = - initialPos + Ball.position;

        return temp;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Goal Net")
        {
            hasScored = true;
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

    IEnumerator resetGame()
    {
        yield return new WaitForSeconds(3);
        if (hasScored)
        {
            goals++;
        }
        else
        {
            defenses++;
        }
        Ball.transform.position = new Vector3(0.0f, 1.0f, -5.27f);
        Ball.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        Ball.freezeRotation = true;
        hasKicked = false;
        hasScored = false;
    }

    IEnumerator initicalPosition()
    {
        yield return new WaitForSeconds(1);
        initialPos = Ball.position;
    }

    public float calcAngle()
    {
        float temp;

        float Hipo = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y + velocity.z * velocity.z);
        float alphaR = Mathf.Asin(velocity.y / Hipo);

        temp =  alphaR;

        
        return temp;
    }



    public float calcRange()
    {
        float temp;
        float time =- velocity.y * 2.0f / gravity;
        temp = calcVelX() * time;
        return temp;
    }

    public float calcVelY()
    {       
        return Mathf.Round(velocity.y);
    }

    public float calcVelX()
    {   
        return Mathf.Round(Mathf.Sqrt(velocity.x * velocity.x + velocity.z * velocity.z));
    }

  
}

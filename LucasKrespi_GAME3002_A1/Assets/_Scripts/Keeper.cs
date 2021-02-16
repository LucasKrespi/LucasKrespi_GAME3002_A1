using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keeper : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject Ball;
    Rigidbody keeper;
    bool hasBall = false;
    Vector3 initialPos;
    void Start()
    {
        keeper = GetComponent<Rigidbody>();
        initialPos = keeper.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Ball.GetComponent<BallBehavior>().hasKicked && !hasBall)
        {

            keeper.velocity = new Vector3(CalculateDirection().normalized.x, CalculateDirection().normalized.y, 0)  * 15;
            
            StartCoroutine(resetGame());
        }
    }

    private Vector3 CalculateDirection()
    {
        Vector3 temp;

        temp = Ball.transform.position - keeper.transform.position;

        return temp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            hasBall = true;
            keeper.velocity = Vector3.zero;
        }
    }
    IEnumerator resetGame()
    {
        yield return new WaitForSeconds(3);
        keeper.rotation = Quaternion.identity;
        keeper.velocity = Vector3.zero;
        keeper.position = initialPos;
        hasBall = false;
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    [SerializeField] private BallBehavior ball;
    [SerializeField] private Transform ballTransform;
    Vector3 mouse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    private Vector3 CalculateDirection()
    {
        Vector3 temp;
        Vector3 mousePos;
        Vector3 PlayerPos;

        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)).normalized;
        PlayerPos = ballTransform.position.normalized;

        temp = - mousePos + PlayerPos;
        
        return temp;
    }

   

}

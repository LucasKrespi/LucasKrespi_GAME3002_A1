﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scri : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] GameObject Ball;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Text.gameObject.name == "Traveled Distance")
        {
            Text.SetText($"Displacement: {Ball.GetComponent<BallBehavior>().distanceTraveled()}");
        }

        if (Text.gameObject.name == "VelX")
        {
            Text.SetText($"VelX0: {Ball.GetComponent<BallBehavior>().calcVelX()} m/s");
        }

        if (Text.gameObject.name == "VelY")
        {
            Text.SetText($"VelY0: {Ball.GetComponent<BallBehavior>().calcVelY()} m/s");
        }

        if (Text.gameObject.name == "Angle")
        {
            Text.SetText($"Angle: {Ball.GetComponent<BallBehavior>().calcAngle() * Mathf.Rad2Deg} °");
        }
        if (Text.gameObject.name == "Score")
        {
            Text.SetText($"Goal: {Ball.GetComponent<BallBehavior>().goals} \n" +
                $"Defenses: {Ball.GetComponent<BallBehavior>().defenses} ");
        }

        if (Text.gameObject.name == "Distance")
        {
            Text.SetText($"Distace: {Ball.GetComponent<BallBehavior>().calcRange()}");
        }
    }
}

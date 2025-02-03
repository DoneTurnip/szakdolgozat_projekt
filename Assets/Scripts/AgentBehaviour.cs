using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class AgentBehaviour : Agent
{
    public float speed = 5f;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 1f, -8);  

    }

    /*public override void CollectObservations(VectorSensor sensor)
    {
    }*/

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionTaken = actions.ContinuousActions;
        float actionSpeed = actionTaken[0];
        float actionSteering = actionTaken[1];

        transform.Translate(Vector3.forward * speed * actionSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0, actionSteering * 180, 0);

        AddReward(-0.01f);
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.ContinuousActions;
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        actions[0] = 0;
        actions[1] = 0;

        if (horizontal == -1)
        {
            actions[1] = -0.5f;
        }
        else if (horizontal == +1)
        {
            actions[1] = +0.5f;
        }

        if (vertical == -1)
        {
            actions[0] = -1;
        }
        else if (vertical == +1)
        {
            actions[0] = +1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Barrier")
        {
            AddReward(-0.5f);
            EndEpisode();
        }
        else if (collision.collider.tag == "Finish")
        {
            AddReward(+1);
            EndEpisode();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class AgentBehaviourTrack_3 : Agent
{
    public float speed = 5f;
    public Transform targetTransfrom;
    public Transform barrier;
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 1f, -7);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransfrom.localPosition - transform.localPosition);

        sensor.AddObservation(GetComponent<Rigidbody>().velocity);

        sensor.AddObservation(barrier.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionTaken = actions.ContinuousActions;
        float actionSpeed = actionTaken[0];
        float actionSteering = actionTaken[1];

        transform.Translate(Vector3.forward * speed * actionSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0, actionSteering * 180, 0);


        Vector3 directionToGoal = (targetTransfrom.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, directionToGoal);
        AddReward(0.01f * dotProduct);

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
            AddReward(-1f);
            EndEpisode();
        }
        else if (collision.collider.tag == "Finish")
        {
            AddReward(+5);
            EndEpisode();
        }
        else if (collision.collider.tag == "Wall")
        {
            AddReward(-1f);
            EndEpisode();
        }
    }
}

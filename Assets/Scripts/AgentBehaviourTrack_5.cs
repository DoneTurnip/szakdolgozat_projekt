using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class AgentBehaviourTrack_5 : Agent
{
    public float speed = 5f;
    public Transform targetTransfrom;
    public Transform hangingBarrier;
    public Transform upDownBarrier;
    public Transform leftRightBarrier;
    public Transform rotatingBarrier;
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 1, -4);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransfrom.localPosition - transform.localPosition);
        sensor.AddObservation(GetComponent<Rigidbody>().velocity);

        // rotatingBarrier
        sensor.AddObservation(rotatingBarrier.eulerAngles.y);
        sensor.AddObservation(rotatingBarrier.transform.localPosition - transform.localPosition);

        // leftRightBarrier
        sensor.AddObservation(leftRightBarrier.localPosition);

        // upDownBarrier
        sensor.AddObservation(upDownBarrier.localPosition);

        // hangingBarrier
        sensor.AddObservation(hangingBarrier.eulerAngles.z);
        sensor.AddObservation(hangingBarrier.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionTaken = actions.ContinuousActions;
        float actionSpeed = actionTaken[0];
        float actionSteering = actionTaken[1];

        transform.Translate(Vector3.forward * speed * actionSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0, actionSteering * 180, 0);

        if (transform.localPosition.z > 7.5f)
        {
            transform.Rotate(0, 90, 0);
        }

        if (transform.localPosition.x > 12.5f)
        {
            transform.Rotate(0, -90, 0);
        }


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

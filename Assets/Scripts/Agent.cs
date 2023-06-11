using UnityEngine;

public class Agent : MonoBehaviour
{
    public AgentNetwork agentNetwork;

    Sensor[] sensorArray;
    Actor[] actorArray;

    void Start()
    {

    }

    public void AgentStart(AgentNetwork network = null)
    {
        sensorArray = GetComponentsInChildren<Sensor>();
        actorArray = GetComponentsInChildren<Actor>();

        if (network == null)
        {
            agentNetwork = new AgentNetwork(sensorArray.Length, 3, 2, actorArray.Length);

            agentNetwork.Randomize(0.1f);
        }
        else
        {
            agentNetwork = new AgentNetwork(network);
        }
    }

    private void AgentUpdate()
    {
        if (agentNetwork != null)
        {
            for (int si = 0; si < sensorArray.Length; si++)
            {
                agentNetwork.SetInput(si, sensorArray[si].Value);
            }

            agentNetwork.CalculateMatrix();

            for (int ai = 0; ai < actorArray.Length; ai++)
            {
                actorArray[ai].Value = agentNetwork.GetOutput(ai);
            }
        }
    }

    private void FixedUpdate()
    {
        AgentUpdate();
    }

    void Update()
    {

    }
}

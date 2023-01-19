using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evoller : MonoBehaviour
{
    public GameObject agentPrefab;
    public UIValue stageCounter;
    public UIValue stageEpochQuality;
    public UIValue stageQuality;
    private int initiatePopulation = 50;

    int stage = 0;

    void Start()
    {
        Application.targetFrameRate = 60;

        InvokeRepeating("Restart", 0, 30);
    }


    public List<Agent> listAgent = new List<Agent>();
    public List<AgentNetwork> network = new List<AgentNetwork>();

    void Restart()
    {
        Agent bestAgent = null;

        network.Clear();

        stage++;
        stageCounter.Value = stage;

        if (listAgent.Count > 0)
        {
            listAgent.Sort(delegate (Agent x, Agent y)
            {
                if (x.transform.position.z < y.transform.position.z) return 1;
                if (x.transform.position.z > y.transform.position.z) return -1;
                return 0;
            });

            float epochQuality = 0;

            foreach(Agent a in listAgent)
            {
                epochQuality += a.transform.position.z;
            }

            stageEpochQuality.Value = epochQuality / listAgent.Count;

            stageQuality.Value = listAgent[0].transform.position.z;

            network.Add(listAgent[0].agentNetwork.Clone());
            network.Add(listAgent[1].agentNetwork.Clone());
            network.Add(listAgent[2].agentNetwork.Clone());
            network.Add(listAgent[3].agentNetwork.Clone());
            network.Add(listAgent[4].agentNetwork.Clone());
        }

        for (int j = 0; j < listAgent.Count; j++)
        {
            Destroy(listAgent[j].gameObject);
        }

        listAgent.Clear();

        for (int i = 0; i < initiatePopulation; i++)
        {
            GameObject go = Instantiate(agentPrefab, transform.position + Vector3.left * i * 4, Quaternion.identity);

            Agent nag = go.GetComponent<Agent>();

            listAgent.Add(nag);

            if (network.Count>0)
            {
                nag.AgentStart(network[i % network.Count]);

                if((i / network.Count) != 0)
                    nag.agentNetwork.Mutate(Mathf.Clamp01(0.12f - stage* (0.1f/50f)));
            }
            else
            {
                nag.AgentStart();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

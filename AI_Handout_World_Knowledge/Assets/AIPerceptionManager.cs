using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPerceptionManager : MonoBehaviour {

	public GameObject Alert;
    public AIMemory agent;

    private void Start()
    {
        agent = GetComponent<AIMemory>();
    }

    // Update is called once per frame
    void PerceptionEvent (PerceptionEvent ev) {

		if(ev.type == global::PerceptionEvent.types.NEW)
		{
			Debug.Log("Saw something NEW");
			Alert.SetActive(true);

            if (agent.memory.ContainsKey(ev.go.name))
            {
                foreach (KeyValuePair<string, blackboard_item> entry in agent.memory)
                {
                    if (entry.Key == ev.go.name)
                    {
                        entry.Value.obj = ev.go;
                        entry.Value.position = ev.go.transform.position;
                        entry.Value.in_memory = false;
                        entry.Value.timestamp = Time.time;
                        break;
                    }
                }
            }
            else
            {
                agent.memory.Add(ev.go.name, new blackboard_item(ev.go, ev.go.transform.position, Time.time);
            }

        }
		else
		{
			Debug.Log("LOST something");
			Alert.SetActive(false);

            foreach (KeyValuePair<string, blackboard_item> entry in agent.memory)
            {
                if (entry.Key == ev.go.name)
                {
                    entry.Value.in_memory = true;
                    break;
                }
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : Singleton<ControlManager>
{
    public GameObject AgentPrefab;

    private GameObject AgentParent;
    private List<Agent> agents = new List<Agent>();

    private Vector2 targetPosition;
    private bool move = false;

    public bool Move
    {
        get
        {
            return move;
        }

        private set
        {
            move = value;
        }
    }

    public Vector2 TargetPosition
    {
        get
        {
            return targetPosition;
        }
        private set
        {
            targetPosition = value;
        }
    }

    public override void Init()
    {
        AgentParent = new GameObject();
        AgentParent.transform.position = Vector3.zero;
    }

    Vector3 ScreenPosToWorldPos(Vector3 pos)
    {
        Debug.Log("worldPosition:" + Camera.main.ScreenToWorldPoint(pos));
        return Camera.main.ScreenToWorldPoint(pos);
    }

    void Update()
    {
        Move = false;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (string.Compare(hit.collider.gameObject.tag, "Obstacle") != 0)
                {
                    GameObject agent = GameObject.Instantiate(AgentPrefab);
                    agent.transform.parent = AgentParent.transform;
                    agent.transform.position = hit.point + Vector3.up * 0.5f;
                    agents.Add(agent.GetComponent<Agent>());
                    Move = true;
                }
            }
        }

        else if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("MouseButtonDown(2)");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (string.Compare(hit.collider.gameObject.tag, "Obstacle") != 0)
                {
                    //Debug.Log(hit.point);
                    targetPosition = new Vector2(hit.point.x, hit.point.z);
                    MapManager.Instance.UpdateFlowField(targetPosition);
                    Move = true;

                    foreach (var agent in agents)
                    {
                        agent.SetTraget(targetPosition);
                    }
                }
            }
        }
    }
}

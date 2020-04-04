using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PigeonController : MonoBehaviour
{
  [Range(0,5)]
  public float randomnessMagnitude;

  private Animator animator;
  private NavMeshAgent agent;
  private Vector3 foodPosition;
  private Vector3 agentPosition;
  private Vector3 randomDirection;
  private int disposition;

  void Awake()
  {
    // ainmator = GetComponent<Animator>;
    agent = GetComponent<NavMeshAgent>();
    foodPosition = GameObject.FindWithTag("sausage-roll").transform.position;
    agentPosition = agent.transform.position;
    randomDirection = new Vector3(Random.Range(-randomnessMagnitude,randomnessMagnitude), 0, Random.Range(-randomnessMagnitude,randomnessMagnitude));
    disposition = Random.Range(2,10);
    Debug.Log(disposition);
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    agentPosition = agent.transform.position;
    // Debug.Log((foodPosition - agentPosition).magnitude);
    // Debug.Log("time" + Time.time + ((int)Time.time % 10 == 0f));

    if((int)Time.time % disposition == 0)
    {
      randomDirection = new Vector3(Random.Range(-randomnessMagnitude,randomnessMagnitude), 0, Random.Range(-randomnessMagnitude,randomnessMagnitude));
    }

    MoveTowards(foodPosition + randomDirection);
    RotateTowards(foodPosition);

  }

  private bool IsInConsumptionRange(Vector3 targetPosition)
  {
    float distance = Vector3.Distance(agentPosition, targetPosition);
    return distance < 0.5f;
  }

  private void MoveTowards(Vector3 targetPosition)
  {
    Vector3 direction = (targetPosition - transform.position).normalized;
    agent.SetDestination(targetPosition);
  }

  private void RotateTowards(Vector3 targetPosition)
  {
    Vector3 direction = (targetPosition - transform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
  }
}

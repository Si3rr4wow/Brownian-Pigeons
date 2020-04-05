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
  private GameObject[] targets;
  private Vector3 foodPosition;
  private Vector3 agentPosition;
  private Vector3 randomDirection;
  private int disposition;

  void Awake()
  {
    // ainmator = GetComponent<Animator>;
    agent = GetComponent<NavMeshAgent>();
    agent.Warp(new Vector3(Random.Range(-20,20), 0, Random.Range(-20,20)));
    agentPosition = agent.transform.position;
    // foodPosition = GameObject.FindWithTag("sausage-roll").transform.position;
    targets = Targets();
    if(TargetsExist(targets))
    {
      foodPosition = ClosestTarget(targets).transform.position;
    }
    randomDirection = new Vector3(Random.Range(-randomnessMagnitude,randomnessMagnitude), 0, Random.Range(-randomnessMagnitude,randomnessMagnitude));
    disposition = Random.Range(2,10);
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if(!TargetsExist(targets)) { return; }

    agentPosition = agent.transform.position;

    if((int)Time.time % disposition == 0)
    {
      randomDirection = new Vector3(Random.Range(-randomnessMagnitude,randomnessMagnitude), 0, Random.Range(-randomnessMagnitude,randomnessMagnitude));
    }

    MoveTowards(foodPosition + randomDirection);
    if(IsInFocusRangeOf(foodPosition))
    {
      RotateTowards(foodPosition);
    }
  }

  private GameObject[] Targets()
  {
    return GameObject.FindGameObjectsWithTag("sausage-roll");
  }

  private bool TargetsExist(GameObject[] targets)
  {
    return targets.Length != 0;
  }

  private GameObject ClosestTarget(GameObject[] targets)
  {
    GameObject closestTarget = targets[0];
    float closestDistanceToAnyTarget = Vector3.Distance(agentPosition, closestTarget.transform.position);
    foreach(GameObject possibleTarget in targets)
    {
      float distanceToTarget = Vector3.Distance(agentPosition, possibleTarget.transform.position);
      if(distanceToTarget > closestDistanceToAnyTarget) { continue; }
      closestDistanceToAnyTarget = distanceToTarget;
      closestTarget = possibleTarget;
  }

  return closestTarget;
}

  private bool IsInFocusRangeOf(Vector3 targetPosition)
  {
    float distance = Vector3.Distance(agentPosition, targetPosition);
    return distance < 5f;
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

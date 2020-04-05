using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PigeonController : MonoBehaviour
{
  [Range(0,50)]
  public float targetDeviationMagnitude;

  [Range(0,10)]
  public float interestRadius;

  private Animator animator;
  private NavMeshAgent agent;
  private GameObject[] targets;
  private Vector3 targetPosition;
  private Vector3 agentPosition;
  private Vector3 targetDeviation;
  private int disposition;

  void Awake()
  {
    // ainmator = GetComponent<Animator>;
    agent = GetComponent<NavMeshAgent>();
    agent.Warp(new Vector3(Random.Range(-20,20), 0, Random.Range(-20,20)));
    agentPosition = agent.transform.position;
    targets = Targets();
    if(TargetsExist())
    {
      targetPosition = ClosestTarget().transform.position;
    }
    SetTargetDeviation();
    disposition = Random.Range(2,10);
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if(!TargetsExist()) { return; }

    if(distanceToClosestTarget() > interestRadius && (int)Time.time % disposition == 0)
    {
      targetPosition = new Vector3(Random.Range(-20,20), 0, Random.Range(-20,20));
    }
    else
    {
      targetPosition = ClosestTarget().transform.position;
    }
    MoveTowardsTarget(targetPosition);
  }

  private void MoveTowardsTarget(Vector3 targetPosition)
  {
    agentPosition = agent.transform.position;

    if((int)Time.time % disposition == 0)
    {
      SetTargetDeviation();
    }

    MoveTowards(targetPosition + targetDeviation);
    if(IsInFocusRangeOf(targetPosition))
    {
      RotateTowards(targetPosition);
    }
  }

  private void SetTargetDeviation()
  {
    targetDeviation = new Vector3(Random.Range(-targetDeviationMagnitude,targetDeviationMagnitude), 0, Random.Range(-targetDeviationMagnitude,targetDeviationMagnitude));
  }

  private GameObject[] Targets()
  {
    return GameObject.FindGameObjectsWithTag("sausage-roll");
  }

  private bool TargetsExist()
  {
    return targets.Length != 0;
  }

  private float distanceToClosestTarget()
  {
    return Vector3.Distance(agentPosition, ClosestTarget().transform.position);
  }

  private GameObject ClosestTarget()
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

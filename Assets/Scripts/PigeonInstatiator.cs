using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonInstatiator : MonoBehaviour
{
  public GameObject PigeonPrefeb;

  [Range(1,100)]
  public int pigeonCount;

  void Start()
  {
    for(int i = 0; i < pigeonCount; i++) {
      Instantiate(PigeonPrefeb, new Vector3(Random.Range(-10,10), 0, Random.Range(-10,10)), Quaternion.identity);
    }
  }
}

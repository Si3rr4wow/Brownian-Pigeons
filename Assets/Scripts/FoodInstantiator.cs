using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInstantiator : MonoBehaviour
{
  public GameObject FoodPrefab;

  private Camera MainCamera;

  void Awake()
  {
     MainCamera = Camera.main;
  }

  void Update()
  {
    if (!Input.GetMouseButtonDown(0)) { return; }
    GameObject spawnedFood = Instantiate(FoodPrefab, MainCamera.transform.position, Quaternion.identity);

    Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
    spawnedFood.GetComponent<Rigidbody>().velocity = Vector3.Scale(ray.direction, new Vector3(10, 10, 10));
  }
}

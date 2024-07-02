using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    protected void Awake()
    {
      transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }
}

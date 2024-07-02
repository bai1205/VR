using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            GameObject.Destroy(gameObject);

            KnapsackData.Instance.money++;
            if (KnapsackData.Instance.money > 3)
            {
                KnapsackData.Instance.money = 3;
            }
            Save.Instance.gameData.gold = KnapsackData.Instance.money;
        }
    }
}

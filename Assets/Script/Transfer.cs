using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfer : MonoBehaviour
{
    public string scenesName;
    public Vector3 TransferPos;
    public Sprite sprite;
    public void Update()
    {
        if (Player.Instance)
        {
            float dis = Vector3.Distance(transform.position, Player.Instance.transform.position);
            if (dis <= 3)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameEvent.Instance.Dispatch(GameEventType.LoadSceneStart, new object[] { scenesName, TransferPos, sprite });

                }
            }
        }

    }
}


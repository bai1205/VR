using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Timer.Instance.PlayTimer(1, () =>
        {
            Player.Instance.AddSkill4();
            Player.Instance.AddSkill3();
            Player.Instance.AddSkill2();
            Player.Instance.AddSkill1();
            Player.Instance.AddSkill5();
            Player.Instance.InitSkill();
        });
    }


}

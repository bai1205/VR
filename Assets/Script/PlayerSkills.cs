using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Timer.Instance.PlayTimer(1, () =>
        {
            foreach (var item in GameManager.Instance.useSkill)
            {
                switch (item)
                {
                    case 1:
                        Player.Instance.AddSkill1();
                        break;
                    case 2:
                        Player.Instance.AddSkill2();
                        break;
                    case 3:
                        Player.Instance.AddSkill3();
                        break;
                    case 4:
                        Player.Instance.AddSkill4();
                        break;
                    case 5:
                        Player.Instance.AddSkill5();
                        break;
                }
            }
            Player.Instance.InitSkill();
        });

    }


}

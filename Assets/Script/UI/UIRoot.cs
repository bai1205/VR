using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot :MonoBehaviour
{
    static Transform transform;
    static Transform recyclePool;
    static Transform workstation;
    static Transform noticestation;
    static bool isInit = false;
    public static void Init() {
        if (transform==null)
        {
            var obj=  Resources.Load<GameObject>("Prefab/UI/UIRoot");
            transform= GameObject.Instantiate(obj).transform;
            DontDestroyOnLoad(transform.gameObject);
        }

        if (recyclePool==null)
        {
            recyclePool = transform.Find("recyclePool");
        }

        if (workstation == null)
        {
            workstation = transform.Find("workstation");
        }

        if (noticestation == null)
        {
            noticestation = transform.Find("noticestation");
        }
        isInit = true;
    }

    public static void SetParent(Transform window, bool isOpen,bool isTipsWindow=false) {
        if (isInit==false)
        {
            Init();
        }

        if (isOpen==true)
        {
            if (isTipsWindow)
            {
                window.SetParent(noticestation, false);
            }
            else
            {
                window.SetParent(workstation, false);
            }
        }
        else
        {
            window.SetParent(recyclePool, false);
        }
    }
}

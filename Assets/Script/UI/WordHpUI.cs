using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WordHpUI : MonoBehaviour
{
    Creature monster;
    Image hp;
    Text info_Text;
    float height;

    public void Init(Creature monster,float height=1.5f)
    {
        this.monster = monster;
        this.height = height;
        hp = transform.Find("hp").GetComponent<Image>();
        info_Text = transform.Find("info").GetComponent<Text>();
    }

    void Update()
    {
        if (monster!=null )
        {
            transform.position = new Vector3(monster.transform.position.x, monster.transform.position.y+height, monster.transform.position.z);
            transform.LookAt(Camera.main.transform.position);
        }
    }

    public void SetHp(float hp,float value=0)
    {
        this.hp.fillAmount = hp;
        if (value!=0)
        {
            info_Text.text = value.ToString();
            Text text = GameObject.Instantiate(info_Text, info_Text.transform.position, info_Text.transform.rotation, transform);
            text.gameObject.SetActive(true);
            text.transform.DOMoveY(info_Text.transform.position.y + height/2, 1).onComplete += () => {
                GameObject.Destroy(text.gameObject);
            };

        }
    }
}

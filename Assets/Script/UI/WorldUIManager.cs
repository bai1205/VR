using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorldUIManager :MonoBehaviour
{
    public static WorldUIManager Instance;
    WordHpUI wordHpUI;
    public void Awake()
    {
        Instance = this;
        wordHpUI = Instantiate(Resources.Load<GameObject>("WordHpUI"), transform).AddComponent<WordHpUI>();
        wordHpUI.gameObject.SetActive(false);
        GameObject.DontDestroyOnLoad(gameObject);

    }

    public WordHpUI GetWordHpUI(Creature monster,float height=1.5f)
    {
        WordHpUI wordHpUI= GameObject.Instantiate(this.wordHpUI, transform);
        wordHpUI.Init(monster,height);
        wordHpUI.gameObject.SetActive(true);
        return wordHpUI;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{

    public class BaseWindow
    {

        protected Transform transform;
        protected string resName;
        protected string name;
        protected bool resident; 
        protected bool visible = false;
        protected WindowType selfType;
        protected ScenesType scenesType;
        protected Button[] buttonList;


        protected virtual void Awake()
        {

            buttonList = transform.GetComponentsInChildren<Button>(true);
            RegisterUIEvent();
        }


        protected virtual void RegisterUIEvent()
        {
            Debug.Log("UI");
        }


        protected virtual void OnAddListener()
        {
            Debug.Log("add game event");
        }

        //移除游戏事件
        protected virtual void OnRemoveListener()
        {
            Debug.Log("Remove game event");
        }

        //每次打开
        protected virtual void OnEnable()
        {
            Debug.Log("Open");
        }

        //每次关闭
        protected virtual void OnDisable()
        {
            Debug.Log("Close");
        }

        //每帧更新
        public virtual void Update(float deltaTime)
        {

        }



        public void Open()
        {
            if (transform == null)
            {
                if (Create())
                {
                    Awake();
                }
            }

            if (transform.gameObject.activeSelf == false)
            {
                UIRoot.SetParent(transform, true, selfType == WindowType.TipsWindow);
                transform.gameObject.SetActive(true);
                visible = true;
                OnEnable();
                OnAddListener();
            }
        }

        public void Close(bool isDestroy = false)
        {
            if (transform==null )
            {
                return;
            }
            if (transform.gameObject.activeSelf == true)
            {
                OnRemoveListener();
                OnDisable();
                if (isDestroy == false)
                {
                    if (resident)
                    {
                        transform.gameObject.SetActive(false);
                        UIRoot.SetParent(transform, false, false);

                    }
                    else
                    {
                        GameObject.Destroy(transform.gameObject);
                        transform = null;
                    }
                }
                else
                {
                    GameObject.Destroy(transform.gameObject);
                    transform = null;
                }


            }
            visible = false;
        }

        public void PreLoad()
        {
            if (transform == null)
            {
                if (Create())
                {
                    Awake();
                }
            }
        }

        public ScenesType GetScenesType()
        {
            return scenesType;
        }

        public WindowType GetWindowType()
        {
            return selfType;
        }

        public Transform GetRoot()
        {
            return transform;
        }
        public bool IsVisible()
        {
            return visible;
        }

        public bool IsREsident()
        {
            return resident;
        }

        private bool Create()
        {
            if (string.IsNullOrEmpty(resName))
            {
                return false;
            }

            if (transform == null)
            {
                var obj = Resources.Load<GameObject>(resName);
                if (obj == null)
                {
                    Debug.LogError($"UI prefab not found: {selfType}");
                    return false;
                }
                transform = GameObject.Instantiate(obj).transform;

                transform.gameObject.SetActive(false);

                UIRoot.SetParent(transform, false, selfType == WindowType.TipsWindow);
                return true;
            }

            return true;
        }


    }
}


public enum WindowType
{
    LoginAndRegisterWindow,
    LoadWindow,
    TipsWindow,
    SettingWindow,
    MainWindow,
    StartWindow,
    ZhanDouWindow,
    ExitWindow,
    KnapsackWindow,
    HintWindow,
    FunctionalWindow,
    SkillWindow,
    LevelWindow,
}



public enum ScenesType
{
    None,
    Login,
    Battle,
    Main,
    bg,
}
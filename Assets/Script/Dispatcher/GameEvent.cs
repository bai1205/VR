using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEventType
{
    LoadSceneStart,
    LoadSceneEixt,
}
public class GameEvent : EventBase<GameEvent, object[], GameEventType>
{
    

}


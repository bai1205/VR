using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class ObstacleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Vector3> vector3s = new List<Vector3>();
    private Obstacle obstacle;
    private Obstacle obstacle1;
    private Gold gold;
    void Start()
    {
        obstacle = transform.Find("obstacle2").GetComponent<Obstacle>();
        obstacle1 = transform.Find("obstacle1").GetComponent<Obstacle>();
        gold = transform.Find("coin").GetComponent<Gold>();
        for (int i = 0; i < 31; i++)
        {
            for (int j = 0; j < 31; j++)
            {
                Vector3 pos = new Vector3(i + transform.position.x, 0, j + transform.position.z);
                if (pos.x>13&& pos.x<16)
                {
                    continue;
                }
                vector3s.Add(new Vector3(i + transform.position.x, 0, j + transform.position.z));
            }
        }
        for (int i = 0; i < 5; i++)
        {
            int index = Random.Range(0, vector3s.Count);
            GameObject.Instantiate(obstacle, vector3s[index], Quaternion.identity, transform).gameObject.SetActive(true);
            vector3s.Remove(vector3s[index]);
        }
        for (int i = 0; i < 20; i++)
        {
            int index = Random.Range(0, vector3s.Count);
            GameObject.Instantiate(obstacle1, vector3s[index], Quaternion.identity, transform).gameObject.SetActive(true);
            vector3s.Remove(vector3s[index]);
        }
        int time1 = Random.Range(1, 120);
        int time2 = Random.Range(1, 120);
        int time3 = Random.Range(1, 120);
        Timer.Instance.PlayTimer(time1, () =>
        {
            if (gameObject != null)
            {
                int index = Random.Range(0, vector3s.Count);
                GameObject.Instantiate(gold, vector3s[index], Quaternion.identity, transform).gameObject.SetActive(true);
                vector3s.Remove(vector3s[index]);
            }
        });

        Timer.Instance.PlayTimer(time2, () =>
        {
            if (gameObject != null)
            {
                int index = Random.Range(0, vector3s.Count);
                GameObject.Instantiate(gold, vector3s[index], Quaternion.identity, transform).gameObject.SetActive(true);
                vector3s.Remove(vector3s[index]);
            }
        });
        Timer.Instance.PlayTimer(time3, () =>
        {
            if (gameObject != null)
            {
                int index = Random.Range(0, vector3s.Count);
                GameObject.Instantiate(gold, vector3s[index], Quaternion.identity, transform).gameObject.SetActive(true);
                vector3s.Remove(vector3s[index]);
            }
        });
    }

}

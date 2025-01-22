using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private GameObject _PlayerPrefab;
    [SerializeField] private float offsetSpeed = 0.5f;
    private List<GameObject> roadList = new List<GameObject>();
    private float offsetVal;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = Vector3.zero;
        InitDefaultRoad();
        offsetVal = offsetSpeed * Time.deltaTime;
    }

    void InitDefaultRoad()
    {
        Vector3 position = Vector3.zero;
        int[] values = { -1, 0, 1 };
        roadList.Add(Instantiate(_roadPrefab, position, Quaternion.identity));
        roadList[0].transform.Find("Obstacle").gameObject.transform.position =
            roadList[roadList.Count - 1].transform.position + Vector3.right * values[Random.Range(0, values.Length)];
        roadList.Add(Instantiate(_roadPrefab, position + Vector3.forward * 20, Quaternion.identity));
        roadList[1].transform.Find("Obstacle").gameObject.transform.position =
            roadList[roadList.Count - 1].transform.position + Vector3.right * values[Random.Range(0, values.Length)];
        roadList.Add(Instantiate(_roadPrefab, position + Vector3.forward * 40, Quaternion.identity));
        roadList[2].transform.Find("Obstacle").gameObject.transform.position =
            roadList[roadList.Count - 1].transform.position + Vector3.right * values[Random.Range(0, values.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        bool isRoadBehind = false;
        foreach (GameObject road in roadList)
        {
            road.transform.position -= offsetVal * Vector3.forward;
            road.transform.position -= offsetVal * Vector3.forward;
            if (CheckRoadBehind(road))
            {
                isRoadBehind = true;
            }
        }
        if (isRoadBehind)
        {
            SetRoadPosition();
        }
    }
    
    private bool CheckRoadBehind(GameObject road)
    {
        if (road.transform.position.z + 10 < _PlayerPrefab.transform.position.z - 20) return true;
        else return false;
    }

    private void SetRoadPosition()
    {
        GameObject firstRoad = roadList[0];
        GameObject obstacle = firstRoad.transform.Find("Obstacle").gameObject;
        roadList.RemoveAt(0);
        roadList.Add(firstRoad);
        roadList[roadList.Count - 1].transform.position = new Vector3(0, 0, roadList[roadList.Count - 2].transform.position.z + 20);
        
        int[] values = { -1, 0, 1 };
        int randomIndex = Random.Range(0, values.Length);  // 0부터 values.Length - 1까지 랜덤 인덱스 생성
        obstacle.transform.position = roadList[roadList.Count - 1].transform.position + Vector3.right * values[randomIndex];
    }
    
}

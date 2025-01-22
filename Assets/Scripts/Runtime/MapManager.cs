using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private GameObject _PlayerPrefab;
    private float offsetSpeed = 1.0f;
    private List<GameObject> roadList = new List<GameObject>();
    private float offsetVal;
    public int unit = 0;
    private bool bIsAlreadyCounted = false;
    private bool isGameStarted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        InitMap();
    }

    public void InitMap()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject) ;
        }
        unit = 0;
        offsetSpeed = 1.0f;
        _PlayerPrefab.transform.position = new Vector3(0, 1, 0);
        InitDefaultRoad();
        offsetVal = offsetSpeed * Time.deltaTime;
        Debug.Log(Time.deltaTime);
    }
    void InitDefaultRoad()
    {
        Vector3 position = Vector3.zero;
        int[] values = { -1, 0, 1 };
        for (int i = 0; i < 3; i++)
        {
            Instantiate(_roadPrefab, position + Vector3.forward * 20 * i, Quaternion.identity).transform.SetParent(this.transform);
            roadList.Add(this.transform.GetChild(i).gameObject);
            roadList[i].transform.Find("Obstacle").gameObject.transform.position +=
                roadList[roadList.Count - 1].transform.position + Vector3.right * values[Random.Range(0, values.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted) return;
        bool isRoadBehind = false;
        for (int i = 0; i < roadList.Count; i++)
        {
            roadList[i].transform.position -= offsetVal * Vector3.forward;
            if (CheckRoadBehind(roadList[i]))
            {
                isRoadBehind = true;
            }
        }
        if (isRoadBehind)
        {
            bIsAlreadyCounted = false;
            SetRoadPosition();
        } 
    }
    private bool CheckRoadBehind(GameObject road)
    {
        if (road.transform.position.z < -15.0f)
        {
            if (!bIsAlreadyCounted)
            {
                unit += 1;
                _PlayerPrefab.gameObject.GetComponent<ScoreManager>().DecreaseGas();
                bIsAlreadyCounted = true;
            }
        }
        if (road.transform.position.z < _PlayerPrefab.transform.position.z - 30)
        {
            return true;
        }
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
        obstacle.transform.position = new Vector3(0,1, 5) +  roadList[roadList.Count - 1].transform.position + Vector3.right * values[randomIndex];
    }

    public void GameStart()
    {
        isGameStarted = true;
    }

    public void GameOver()
    {
        isGameStarted = false;
        foreach (GameObject road in roadList)
        {
            Destroy(road);
        }
        roadList.Clear();
    }
    
}

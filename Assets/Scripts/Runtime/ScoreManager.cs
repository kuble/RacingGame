using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int gas = 100;

    [SerializeField] private TMP_Text gasText;

    [SerializeField] private UIManager _UIManager;
    [SerializeField] private MapManager _MapManager;
    // Start is called before the first frame update
    void Start()
    {
        InitScore();
    }

    public void InitScore()
    {
        gas = 100;
        UpdateGasText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGasText()
    {
        gasText.text = "Gas : " + gas.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        gas += 30;
        UpdateGasText();
    }

    public void DecreaseGas()
    {
        gas -= 10;
        if (gas <= 0)
        {
            _UIManager.ConvertToGameOver();
            _MapManager.GameOver();
        }
        UpdateGasText();
        
    }
}

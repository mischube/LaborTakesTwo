using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVisibility : MonoBehaviour
{
    [SerializeField]
    private RawImage _hand;
    [SerializeField]
    private RawImage _weapon1;
    [SerializeField]
    private RawImage _weapon2;
    [SerializeField]
    private RawImage _weapon3;
    [SerializeField]
    private RawImage _currentlySelectedBorder;
    [SerializeField] 
    private GameObject _uiManager;
    [SerializeField]
    private List<Transform> _imagePositions = new List<Transform>();
    private bool _uiEnabled = false;
    
    
    private void Start()
    {
        _imagePositions.Add(_hand.transform);
        _imagePositions.Add(_weapon1.transform);
        _imagePositions.Add(_weapon2.transform);
        _imagePositions.Add(_weapon3.transform);
    }
    

    public void EnableAllUI()
    {
        _currentlySelectedBorder.transform.gameObject.SetActive(true);
        _uiEnabled = true;
        TurnUIElementsAround(1);
    }

    public void TurnUIElementsAround(int weaponNumber)
    {
        foreach (Transform uielements in _imagePositions)
        {
            uielements.transform.gameObject.SetActive(false);
        }
        if (_uiEnabled)
        {
            _imagePositions[weaponNumber].transform.gameObject.SetActive(true);
        }
    }
}

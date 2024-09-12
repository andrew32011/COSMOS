using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToNextLevel : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelDatas;
    [SerializeField] private Sprite[] _planetSprite;
    [SerializeField] private Sprite[] _planetDecor1;
    [SerializeField] private Sprite[] _planetDecor2;
    [SerializeField] private GameObject[] decorsObj;
    [SerializeField] private GravityController _gravityController;
    public int currentLevel;
    [SerializeField] private LevelData _LevelData;
    GameObject _Planet;
    [SerializeField] private bool Switch;
    [SerializeField] private TextMeshProUGUI LevelIndicator;
    private void Start()
    {
        Instantiate(_levelDatas[currentLevel]);
        LevelIndicator.text = currentLevel.ToString();
        _gravityController = GameObject.FindFirstObjectByType<GravityController>();
        _Planet = GameObject.FindGameObjectWithTag("Planet");

        foreach (GameObject decor in decorsObj)
        {
            SetDecor(decor);
            //RotateTowards(Vector3.zero);
        }
    }
    private void Update()
    {
        
        if (_levelDatas[currentLevel].GetComponent<LevelData>() != null)
        {
            _LevelData = _levelDatas[currentLevel].GetComponent<LevelData>();
            LevelIndicator.text = currentLevel.ToString();
            if (_gravityController.JumpForse >= _LevelData.JumpNeedToMove)
            {
                if (Switch == false) 
                {
                    SwitchLevel(_LevelData);
                    
                }
                
            }

            float distanceToPlanet = Vector3.Distance(_gravityController.transform.position, _Planet.transform.position);
            //if (distanceToPlanet < _Planet.GetComponent<CircleCollider2D>().radius * 2 & Switch == true)
            //{
            //    SetNormal();
            //    Switch = false;
            //}

            if (_gravityController.Isgrownded)
            {
                _gravityController.GravityForse = _LevelData.GravityModifier;
            }
        }
        
        
    }

    private void SwitchLevel(LevelData levelDestr)
    {
        Instantiate(_levelDatas[currentLevel + 1]);
        currentLevel++;
        LevelIndicator.text = currentLevel.ToString();
        _LevelData = _levelDatas[currentLevel].GetComponent<LevelData>();
        //levelDestr.gameObject.SetActive(false); пока не работает
        _gravityController.GravityForse = 0;
        //_gravityController.JumpForse = 15;
        Invoke(nameof(NewLevelSet), 5f);
        
    }
    //private void SetNormal()
    //{
        
    //    _gravityController.GravityForse = 0f;
    //    Switch = false;
        
    //}
    private void NewLevelSet()
    {
        Sprite NewSprite = _planetSprite[currentLevel];
        
        _Planet.GetComponent<SpriteRenderer>().sprite = NewSprite;
        foreach (GameObject decor in decorsObj)
        {
            SetDecor(decor);
        }
        _gravityController.GravityForse = 5;
        _gravityController.JumpForse = 0;
        Switch = true;

    }
    private void SetDecor(GameObject decor)
    {
        Sprite Decor1 = _planetDecor1[currentLevel];
        Sprite Decor2 = _planetDecor2[currentLevel];
        if (decor.GetComponent<SpriteRenderer>() != null)
        {
            if (decor.GetComponent<SpriteRenderer>().gameObject.tag == "Decor1")
            {
                decor.GetComponent<SpriteRenderer>().sprite = Decor1;
            }
            if (decor.GetComponent<SpriteRenderer>().gameObject.tag == "Decor2")
            {
                decor.GetComponent<SpriteRenderer>().sprite = Decor2;
            }
        }
    }
    //void RotateTowards(Vector3 targetPosition)
    //{
    //    // Calculate the direction from the object to the target point  
    //    Vector3 direction = targetPosition - transform.position;
    //    direction.Normalize();

    //    // Calculate the angle between the object's forward direction and the direction to the target  
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //    // Rotate the object to face the target point  
    //    transform.rotation = Quaternion.Euler(0f, 0f, angle);
    //}
}

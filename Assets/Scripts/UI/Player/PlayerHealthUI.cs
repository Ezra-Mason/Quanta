using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private GameObject _heartUI;
    [SerializeField] private GameObject[] _hearts;
    [SerializeField] private float _spacing;
    [SerializeField] private UnitStats _unitStats;
    private int _maxHealth;
    private int _currentHealth;
    [SerializeField] private IntVariable _currentHealthVar;
    // Start is called before the first frame update
    void Start()
    {
        SetUpHearts();
    }
 
    private void SetUpHearts()
    {
        _maxHealth = _unitStats.MaxHealth;
        _hearts = new GameObject[_maxHealth];
        for (int i = 0; i < _maxHealth; i++)
        {
            Vector3 position = new Vector3(i * _spacing, 0f, 0f);

            _hearts[i] = Instantiate(_heartUI, transform.position, Quaternion.identity, transform);
            if (_hearts[i].TryGetComponent<RectTransform> (out RectTransform rectTransform))
            {
                rectTransform.localPosition = position;
            }
            Debug.Log("player health ui = " + i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHealth !=_currentHealthVar.Value)
        {
            _currentHealth = _currentHealthVar.Value;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < _maxHealth; i++)
        {
            if (i + 1 >_currentHealth)
            {
                _hearts[i].SetActive(false);
            }
            else
            {
                _hearts[i].SetActive(true);
            }
        }
    }
}

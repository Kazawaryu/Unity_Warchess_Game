using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField]
    public float _offsetX;

    [SerializeField]
    public float _offsetY;


    Slider _sefBar;
    Transform _target;

    public static HealthBar Instance;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        _sefBar = GetComponent<Slider>(); 

    }

    public void newBar()
    {
        _sefBar = GetComponent<Slider>();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void changeValue(int currentValue, int maxValue)
    {
        _sefBar.value = currentValue *1.0f/ maxValue;
    }


    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            Vector3 desPos = Camera.main.WorldToScreenPoint(_target.position);
            transform.position = desPos + Vector3.up * _offsetY + Vector3.right * _offsetX;
        }
    }
}

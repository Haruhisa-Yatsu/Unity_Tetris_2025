using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public const int WIDTH = 10;
    public const int HEIGHT = 20;

    /// <summary>
    /// Mino�̃v���n�u
    /// </summary>
    [SerializeField]
    private Mino _minoPrefab;


    // Start is called before the first frame update
    void Start()
    {
        // �~�m�̐�������
        // Field�̎q�Ƃ��Đ�������
        Instantiate(_minoPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

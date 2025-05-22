using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public const int WIDTH = 10;
    public const int HEIGHT = 20;

    /// <summary>
    /// Minoのプレハブ
    /// </summary>
    [SerializeField]
    private Mino _minoPrefab;


    // Start is called before the first frame update
    void Start()
    {
        // ミノの生成処理
        // Fieldの子として生成する
        Instantiate(_minoPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

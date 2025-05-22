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

    /// <summary>
    /// FieldBlockのプレハブ
    /// </summary>
    [SerializeField]
    private FieldBlock _fieldBlockPrefab;


    // Start is called before the first frame update
    void Start()
    {
        // ミノの生成処理
        // Fieldの子として生成する
        Instantiate(_minoPrefab, transform);

        // 盤面の黒部分すべてに
        // _fieldBlockPrefabを敷き詰める処理を記述せよ

        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                // Instantiateの戻り値を保管
                FieldBlock fieldBlock = Instantiate(_fieldBlockPrefab, transform);
                
                //　保管したFieldBlockからRectTransformを取得
                RectTransform rect = fieldBlock.transform as RectTransform;
                
                // ローカルポジションを更新
                rect.localPosition = new Vector3(j, i, 0);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

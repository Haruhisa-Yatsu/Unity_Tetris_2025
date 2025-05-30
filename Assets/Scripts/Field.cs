using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    /// <summary>
    /// 盤面の横幅
    /// </summary>
    public const int WIDTH = 10;

    /// <summary>
    /// 盤面の縦幅
    /// </summary>
    public const int HEIGHT = 20;

    /// <summary>
    /// FieldBlockを保管しておく2次元配列
    /// </summary>
    private FieldBlock[,] _fieldBlockArray;

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

    /// <summary>
    /// 指定した座標のブロックの表示非表示設定
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="enable"></param>
    public void SetFieldBlockEnable(int x, int y, bool enable)
    {
        _fieldBlockArray[x, y].SetEnable(enable);
    }

    /// <summary>
    /// 指定したブロックが表示されているかどうか
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool CheckFieldBlockEnable(int x, int y)
    {
        if (y < 0)
        {
            return true;
        }

        if (y >= HEIGHT)
        {
            return false;
        }
        
        if (x < 0)
        {
            return true;
        }

        if (x >= WIDTH)
        {
            return true;
        }


        return _fieldBlockArray[x, y].GetEnable();
    }

    /// <summary>
    /// ブロックの着地確認処理
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool CheckLanding(int x, int y)
    {
        if (y <= 0)
        {
            return true;
        }

        if(y >= HEIGHT)
        {
            return false;
        }

        return CheckFieldBlockEnable(x, y - 1);
    }

    /// <summary>
    /// 指定した行がすべて埋まっているか
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool CheckLine(int y)
    {
        for (int i = 0; i < WIDTH; i++)
        {
            if (!CheckFieldBlockEnable(i, y))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 指定した行のブロックを削除する
    /// </summary>
    /// <param name="y"></param>
    public void EraceLine(int y)
    {
        for (int j = y; j < HEIGHT; j++)
        {
            for (int i = 0; i < WIDTH; i++)
            {
                if (j == HEIGHT - 1)
                {
                    SetFieldBlockEnable(i, j, false);
                }
                else
                {
                    SetFieldBlockEnable(i, j, CheckFieldBlockEnable(i, j + 1));
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // 盤面の黒部分すべてに
        // _fieldBlockPrefabを敷き詰める処理を記述せよ

        // fieldBlock配列の初期化
        _fieldBlockArray = new FieldBlock[WIDTH, HEIGHT];

        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                // Instantiateの戻り値を保管
                FieldBlock fieldBlock = Instantiate(_fieldBlockPrefab, transform);

                // 最初は非表示
                fieldBlock.SetEnable(false);

                //　保管したFieldBlockからRectTransformを取得
                RectTransform rect = fieldBlock.transform as RectTransform;

                // ローカルポジションを更新
                rect.localPosition = new Vector3(j, i, 0);

                // 2次元配列にfieldBlockを保管
                _fieldBlockArray[j, i] = fieldBlock;
            }
        }

        // ミノの生成処理
        // Fieldの子として生成する
        Mino mino = Instantiate(_minoPrefab, transform);
        mino.SetField(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

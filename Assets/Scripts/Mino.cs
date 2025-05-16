using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour
{
    /// <summary>
    /// 位置縦
    /// </summary>
    private int _posY;

    /// <summary>
    /// 位置横
    /// </summary>
    private int _posX;

    /// <summary>
    /// 落下のカウンタ
    /// </summary>
    private float _fallCount;

    /// <summary>
    /// 落下の秒数
    /// </summary>
    [SerializeField]
    private float _fallSecond;

    /// <summary>
    /// 2D用のTransform
    /// </summary>
    private RectTransform _rect;

    // Start is called before the first frame update
    void Start()
    {
        _posX = Field.WIDTH / 2;
        _posY = Field.HEIGHT - 1;

        _fallCount = 0.0f;

        _rect = transform as RectTransform;

        PositionUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Right();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Left();
        }

        _fallCount += Time.deltaTime;

        if (_fallCount > _fallSecond)
        {
            Fall();
            _fallCount -= _fallSecond;
        }
    }

    /// <summary>
    /// 落下処理
    /// </summary>
    private void Fall()
    {
        if (CheckBlock(_posX, _posY - 1))
        {
            // TODO:着地処理

            return;
        }

        _posY -= 1;
        PositionUpdate();
    }

    /// <summary>
    /// 右移動
    /// </summary>
    private void Right()
    {
        if (CheckBlock(_posX + 1, _posY))
        {
            return;
        }

        _posX += 1;
        PositionUpdate();
    }

    /// <summary>
    /// 右移動
    /// </summary>
    private void Left()
    {
        if (CheckBlock(_posX - 1, _posY))
        {
            return;
        }

        _posX -= 1;
        PositionUpdate();
    }

    /// <summary>
    /// 指定した座標にブロックがあるか
    /// </summary>
    /// <returns></returns>
    private bool CheckBlock(int x, int y)
    {
        // 左壁の判定
        if (x < 0)
        {
            return true;
        }

        // 右壁の判定
        if (x >= Field.WIDTH)
        {
            return true;
        }

        // 底の判定
        if (y < 0)
        {
            return true;
        }

        // TODO:盤面のブロックとの判定

        return false;
    }

    /// <summary>
    /// ミノの位置を更新
    /// </summary>
    private void PositionUpdate()
    {
        _rect.localPosition = new Vector3(_posX, _posY, 0);
    }
}

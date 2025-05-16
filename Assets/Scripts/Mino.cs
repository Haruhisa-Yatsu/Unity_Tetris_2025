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
        _posX = 4;
        _posY = 19;

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
        _posY -= 1;
        PositionUpdate();
    }

    /// <summary>
    /// 右移動
    /// </summary>
    private void Right()
    {
        _posX += 1;
        PositionUpdate();
    }

    /// <summary>
    /// 右移動
    /// </summary>
    private void Left()
    {
        _posX -= 1;
        PositionUpdate();
    }

    /// <summary>
    /// ミノの位置を更新
    /// </summary>
    private void PositionUpdate()
    {
        _rect.localPosition = new Vector3(_posX, _posY, 0);
    }
}

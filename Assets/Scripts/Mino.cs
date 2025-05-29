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
    /// 高速落下の秒数
    /// </summary>
    [SerializeField]
    private float _fastFallSecond;

    /// <summary>
    /// 2D用のTransform
    /// </summary>
    private RectTransform _rect;

    /// <summary>
    /// Fieldの参照
    /// </summary>
    private Field _field;

    /// <summary>
    /// Fieldの参照をセットする
    /// </summary>
    /// <param name="field"></param>
    public void SetField(Field field)
    {
        _field = field;
    }



    /// <summary>
    /// 最初の1フレーム目のみ実行される
    /// </summary>
    void Start()
    {
        PositionInit();

        _fallCount = 0.0f;

        _rect = transform as RectTransform;

        PositionUpdate();
    }

    /// <summary>
    /// 初期位置にミノを飛ばす
    /// </summary>
    private void PositionInit()
    {
        _posX = Field.WIDTH / 2;
        _posY = Field.HEIGHT - 1;
    }

    /// <summary>
    /// 毎フレーム実行される
    /// </summary>
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

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (_fallCount > _fastFallSecond)
            {
                Fall();
                _fallCount = 0;
            }
        }
        else
        {
            if (_fallCount > _fallSecond)
            {
                Fall();
                _fallCount -= _fallSecond;
            }
        }
    }

    /// <summary>
    /// 落下処理
    /// </summary>
    private void Fall()
    {
        if (CheckBlock(_posX, _posY - 1))
        {
            return;
        }

        if (_field.CheckLanding(_posX, _posY))
        {
            _field.SetFieldBlockEnable(_posX, _posY, true);

            for (int i = 0; i < Field.HEIGHT; i++)
            {
                if (_field.CheckLine(i))
                {
                    _field.EraceLine(i);
                }
            }

            PositionInit();
            PositionUpdate();

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

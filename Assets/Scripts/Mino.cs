using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour
{

    public enum RotateAngle
    {
        _0,
        _90,
        _180,
        _270,
    }

    public enum MinoType
    {
        T,
        I,
        O,
        S,
        Z,
        J,
        L,

        MAX
    }

    public int[,,] _shapeData =
    {
        //T の形データ
        {
            { 1,0},
            { 0,0},
            { -1,0},
            { 0,1},
        },
        //I の形データ
        {
            { 0,2},
            { 0,1},
            { 0,0},
            { 0,-1},
        },
        //O の形データ
        {
            { 0,0},
            { -1,0},
            { 0,-1},
            { -1,-1},
        },
        //S の形データ
        {
            { 1,1},
            { 0,1},
            { 0,0},
            { -1,0},
        },
        //Z の形データ
        {
            { -1,1},
            { 0,1},
            { 0,0},
            { 1,0},
        },
        //J の形データ
        {
            { -1,1},
            { -1,0},
            { 0,0},
            { 1,0},
        },
        //L の形データ
        {
            { 1,1},
            { 1,0},
            { 0,0},
            { -1,0},
        },
    };

    /// <summary>
    /// BlockRootをUnityEditorから指定する
    /// </summary>
    [SerializeField]
    private RectTransform[] _blockRootArray;

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
    /// 現在のミノの形
    /// </summary>
    private int[,] _currentShapeData;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var tempShape = ShapeRotate(_currentShapeData, RotateAngle._270);

            bool overlap = false;

            for (int j = 0; j < 4; j++)
            {
                int px = _posX + tempShape[j, 0];
                int py = _posY + tempShape[j, 1];

                if (_field.CheckFieldBlockEnable(px, py))
                {
                    overlap = true;
                    break;
                }
            }

            if (!overlap)
            {
                _currentShapeData = tempShape;
                BlockRootUpdate();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            _currentShapeData = ShapeRotate(_currentShapeData,RotateAngle._90);
            BlockRootUpdate();
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
    /// Fieldの参照をセットする
    /// </summary>
    /// <param name="field"></param>
    public void SetField(Field field)
    {
        _field = field;
    }

    /// <summary>
    /// 初期位置にミノを飛ばす
    /// </summary>
    private void PositionInit()
    {
        _currentShapeData = GetShape((MinoType)Random.Range(0, (int)MinoType.MAX));

        BlockRootUpdate();

        _posX = Field.WIDTH / 2;
        _posY = Field.HEIGHT - 1;
    }

    /// <summary>
    /// ブロックルートの座標を更新
    /// </summary>
    private void BlockRootUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            var newPos = new Vector3(_currentShapeData[i, 0], _currentShapeData[i, 1], 0);

            _blockRootArray[i].localPosition = newPos;
        }
    }

    /// <summary>
    /// 指定したMinoTypeの形データを取得する。
    /// </summary>
    /// <param name="minoType"></param>
    /// <returns></returns>
    public int[,] GetShape(MinoType minoType)
    {
        int[,] ret = new int[4, 2];

        for (int i = 0; i < 4; i++)
        {
            ret[i, 0] = _shapeData[(int)minoType, i, 0];
            ret[i, 1] = _shapeData[(int)minoType, i, 1];
        }

        return ret;
    }

    /// <summary>
    /// 与えられたShapeDataをRotateAngle分回転させて返す関数
    /// </summary>
    /// <param name="shape"></param>
    /// <param name="rotateAngle"></param>
    /// <returns></returns>
    public int[,] ShapeRotate(int[,] shape, RotateAngle rotateAngle)
    {
        int[,] ret = new int[4, 2];

        switch (rotateAngle)
        {
            case RotateAngle._0:

                return shape;

            case RotateAngle._90:

                for (int i = 0; i < 4; i++)
                {
                    ret[i, 0] = -shape[i, 1];
                    ret[i, 1] = shape[i, 0];
                }

                break;

            case RotateAngle._180:

                for (int i = 0; i < 4; i++)
                {
                    ret[i, 0] = -shape[i, 0];
                    ret[i, 1] = -shape[i, 1];
                }

                break;

            case RotateAngle._270:

                for (int i = 0; i < 4; i++)
                {
                    ret[i, 0] = shape[i, 1];
                    ret[i, 1] = -shape[i, 0];
                }

                break;

            default:

                break;
        }

        return ret;
    }


    /// <summary>
    /// 落下処理
    /// </summary>
    private void Fall()
    {
        bool landing = false;

        for (int j = 0; j < 4; j++)
        {
            int px = _posX + _currentShapeData[j, 0];
            int py = _posY + _currentShapeData[j, 1];

            if (_field.CheckLanding(px, py))
            {
                landing = true;
                break;
            }
        }

        if (landing)
        {
            for (int j = 0; j < 4; j++)
            {
                int px = _posX + _currentShapeData[j, 0];
                int py = _posY + _currentShapeData[j, 1];

                _field.SetFieldBlockEnable(px, py, true);
            }

            for (int i = 0; i < Field.HEIGHT; i++)
            {
                if (_field.CheckLine(Field.HEIGHT - i - 1))
                {
                    _field.EraceLine(Field.HEIGHT - i - 1);
                }
            }

            PositionInit();
            PositionUpdate();
        }
        else
        {
            _posY -= 1;
            PositionUpdate();
        }
    }

    /// <summary>
    /// 右移動
    /// </summary>
    private void Right()
    {
        for (int j = 0; j < 4; j++)
        {
            int px = _posX + _currentShapeData[j, 0];
            int py = _posY + _currentShapeData[j, 1];

            if (CheckBlock(px + 1, py))
            {
                return;
            }
        }

        _posX += 1;
        PositionUpdate();
    }

    /// <summary>
    /// 右移動
    /// </summary>
    private void Left()
    {
        for (int j = 0; j < 4; j++)
        {
            int px = _posX + _currentShapeData[j, 0];
            int py = _posY + _currentShapeData[j, 1];

            if (CheckBlock(px - 1, py))
            {
                return;
            }
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

        if (_field.CheckFieldBlockEnable(x, y))
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

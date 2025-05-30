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
        //T �̌`�f�[�^
        {
            { 1,0},
            { 0,0},
            { -1,0},
            { 0,1},
        },
        //I �̌`�f�[�^
        {
            { 0,2},
            { 0,1},
            { 0,0},
            { 0,-1},
        },
        //O �̌`�f�[�^
        {
            { 0,0},
            { -1,0},
            { 0,-1},
            { -1,-1},
        },
        //S �̌`�f�[�^
        {
            { 1,1},
            { 0,1},
            { 0,0},
            { -1,0},
        },
        //Z �̌`�f�[�^
        {
            { -1,1},
            { 0,1},
            { 0,0},
            { 1,0},
        },
        //J �̌`�f�[�^
        {
            { -1,1},
            { -1,0},
            { 0,0},
            { 1,0},
        },
        //L �̌`�f�[�^
        {
            { 1,1},
            { 1,0},
            { 0,0},
            { -1,0},
        },
    };

    /// <summary>
    /// BlockRoot��UnityEditor����w�肷��
    /// </summary>
    [SerializeField]
    private RectTransform[] _blockRootArray;

    /// <summary>
    /// �ʒu�c
    /// </summary>
    private int _posY;

    /// <summary>
    /// �ʒu��
    /// </summary>
    private int _posX;

    /// <summary>
    /// �����̃J�E���^
    /// </summary>
    private float _fallCount;

    /// <summary>
    /// �����̕b��
    /// </summary>
    [SerializeField]
    private float _fallSecond;

    /// <summary>
    /// ���������̕b��
    /// </summary>
    [SerializeField]
    private float _fastFallSecond;

    /// <summary>
    /// 2D�p��Transform
    /// </summary>
    private RectTransform _rect;

    /// <summary>
    /// Field�̎Q��
    /// </summary>
    private Field _field;

    /// <summary>
    /// ���݂̃~�m�̌`
    /// </summary>
    private int[,] _currentShapeData;

    /// <summary>
    /// �ŏ���1�t���[���ڂ̂ݎ��s�����
    /// </summary>
    void Start()
    {
        PositionInit();

        _fallCount = 0.0f;

        _rect = transform as RectTransform;

        PositionUpdate();
    }

    /// <summary>
    /// ���t���[�����s�����
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
    /// Field�̎Q�Ƃ��Z�b�g����
    /// </summary>
    /// <param name="field"></param>
    public void SetField(Field field)
    {
        _field = field;
    }

    /// <summary>
    /// �����ʒu�Ƀ~�m���΂�
    /// </summary>
    private void PositionInit()
    {
        _currentShapeData = GetShape((MinoType)Random.Range(0, (int)MinoType.MAX));

        BlockRootUpdate();

        _posX = Field.WIDTH / 2;
        _posY = Field.HEIGHT - 1;
    }

    /// <summary>
    /// �u���b�N���[�g�̍��W���X�V
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
    /// �w�肵��MinoType�̌`�f�[�^���擾����B
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
    /// �^����ꂽShapeData��RotateAngle����]�����ĕԂ��֐�
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
    /// ��������
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
    /// �E�ړ�
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
    /// �E�ړ�
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
    /// �w�肵�����W�Ƀu���b�N�����邩
    /// </summary>
    /// <returns></returns>
    private bool CheckBlock(int x, int y)
    {
        // ���ǂ̔���
        if (x < 0)
        {
            return true;
        }

        // �E�ǂ̔���
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
    /// �~�m�̈ʒu���X�V
    /// </summary>
    private void PositionUpdate()
    {
        _rect.localPosition = new Vector3(_posX, _posY, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour
{
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
    /// Field�̎Q�Ƃ��Z�b�g����
    /// </summary>
    /// <param name="field"></param>
    public void SetField(Field field)
    {
        _field = field;
    }



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
    /// �����ʒu�Ƀ~�m���΂�
    /// </summary>
    private void PositionInit()
    {
        _posX = Field.WIDTH / 2;
        _posY = Field.HEIGHT - 1;
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
    /// ��������
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
    /// �E�ړ�
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
    /// �E�ړ�
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

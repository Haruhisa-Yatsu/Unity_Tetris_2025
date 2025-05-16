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
    /// 2D�p��Transform
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
    /// ��������
    /// </summary>
    private void Fall()
    {
        if (CheckBlock(_posX, _posY - 1))
        {
            // TODO:���n����

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

        // ��̔���
        if (y < 0)
        {
            return true;
        }

        // TODO:�Ֆʂ̃u���b�N�Ƃ̔���

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

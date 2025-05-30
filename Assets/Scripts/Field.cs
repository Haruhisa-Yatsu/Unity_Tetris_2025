using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    /// <summary>
    /// �Ֆʂ̉���
    /// </summary>
    public const int WIDTH = 10;

    /// <summary>
    /// �Ֆʂ̏c��
    /// </summary>
    public const int HEIGHT = 20;

    /// <summary>
    /// FieldBlock��ۊǂ��Ă���2�����z��
    /// </summary>
    private FieldBlock[,] _fieldBlockArray;

    /// <summary>
    /// Mino�̃v���n�u
    /// </summary>
    [SerializeField]
    private Mino _minoPrefab;

    /// <summary>
    /// FieldBlock�̃v���n�u
    /// </summary>
    [SerializeField]
    private FieldBlock _fieldBlockPrefab;

    /// <summary>
    /// �w�肵�����W�̃u���b�N�̕\����\���ݒ�
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="enable"></param>
    public void SetFieldBlockEnable(int x, int y, bool enable)
    {
        _fieldBlockArray[x, y].SetEnable(enable);
    }

    /// <summary>
    /// �w�肵���u���b�N���\������Ă��邩�ǂ���
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
    /// �u���b�N�̒��n�m�F����
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
    /// �w�肵���s�����ׂĖ��܂��Ă��邩
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
    /// �w�肵���s�̃u���b�N���폜����
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
        // �Ֆʂ̍��������ׂĂ�
        // _fieldBlockPrefab��~���l�߂鏈�����L�q����

        // fieldBlock�z��̏�����
        _fieldBlockArray = new FieldBlock[WIDTH, HEIGHT];

        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                // Instantiate�̖߂�l��ۊ�
                FieldBlock fieldBlock = Instantiate(_fieldBlockPrefab, transform);

                // �ŏ��͔�\��
                fieldBlock.SetEnable(false);

                //�@�ۊǂ���FieldBlock����RectTransform���擾
                RectTransform rect = fieldBlock.transform as RectTransform;

                // ���[�J���|�W�V�������X�V
                rect.localPosition = new Vector3(j, i, 0);

                // 2�����z���fieldBlock��ۊ�
                _fieldBlockArray[j, i] = fieldBlock;
            }
        }

        // �~�m�̐�������
        // Field�̎q�Ƃ��Đ�������
        Mino mino = Instantiate(_minoPrefab, transform);
        mino.SetField(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

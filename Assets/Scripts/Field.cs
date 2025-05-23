using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public const int WIDTH = 10;
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

    // Start is called before the first frame update
    void Start()
    {
        // �~�m�̐�������
        // Field�̎q�Ƃ��Đ�������
        Mino mino = Instantiate(_minoPrefab, transform);
        mino.SetField(this);

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

    }

    // Update is called once per frame
    void Update()
    {

    }
}

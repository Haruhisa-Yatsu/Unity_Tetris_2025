using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public const int WIDTH = 10;
    public const int HEIGHT = 20;

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


    // Start is called before the first frame update
    void Start()
    {
        // �~�m�̐�������
        // Field�̎q�Ƃ��Đ�������
        Instantiate(_minoPrefab, transform);

        // �Ֆʂ̍��������ׂĂ�
        // _fieldBlockPrefab��~���l�߂鏈�����L�q����

        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                // Instantiate�̖߂�l��ۊ�
                FieldBlock fieldBlock = Instantiate(_fieldBlockPrefab, transform);
                
                //�@�ۊǂ���FieldBlock����RectTransform���擾
                RectTransform rect = fieldBlock.transform as RectTransform;
                
                // ���[�J���|�W�V�������X�V
                rect.localPosition = new Vector3(j, i, 0);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

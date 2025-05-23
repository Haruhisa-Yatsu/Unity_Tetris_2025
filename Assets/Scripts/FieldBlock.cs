using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Image�N���X���g�p����̂ɕK�v
using UnityEngine.UI;

public class FieldBlock : MonoBehaviour
{
    // �摜�̕\�� �� Image
    // Image�̐F���w��ł���悤�ɂ���
    // �\����\���̐؂�ւ�

    /// <summary>
    /// Image�R���|�[�l���g
    /// </summary>
    [SerializeField]
    private Image _image;

    /// <summary>
    /// �\����\���̐؂�ւ�
    /// </summary>
    public void SetEnable(bool enable)
    {
        _image.enabled = enable;
    }

    /// <summary>
    /// �u���b�N���\������Ă��邩�ǂ���
    /// </summary>
    /// <returns></returns>
    public bool GetEnable()
    {
        return _image.enabled;
    }

    /// <summary>
    /// Image�̐F��ݒ肷��
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color)
    {
        _image.color = color;
    }

}

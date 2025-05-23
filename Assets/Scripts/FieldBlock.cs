using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Imageクラスを使用するのに必要
using UnityEngine.UI;

public class FieldBlock : MonoBehaviour
{
    // 画像の表示 → Image
    // Imageの色を指定できるようにする
    // 表示非表示の切り替え

    /// <summary>
    /// Imageコンポーネント
    /// </summary>
    [SerializeField]
    private Image _image;

    /// <summary>
    /// 表示非表示の切り替え
    /// </summary>
    public void SetEnable(bool enable)
    {
        _image.enabled = enable;
    }

    /// <summary>
    /// ブロックが表示されているかどうか
    /// </summary>
    /// <returns></returns>
    public bool GetEnable()
    {
        return _image.enabled;
    }

    /// <summary>
    /// Imageの色を設定する
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color)
    {
        _image.color = color;
    }

}

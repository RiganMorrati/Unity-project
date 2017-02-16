using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public Text text;
    public Image img;

    public byte Value
    {
        get
        {
            byte a;
            return byte.TryParse(text.text, out a) ? a : (byte)0;
        }
        set
        {
            text.text = value.ToString();
        }
    }

    Quaternion startrotation;
    void Start()
    {
        startrotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = startrotation;
    }

    public void SetColor(Color color)
    {
        img.color = color;
    }
}
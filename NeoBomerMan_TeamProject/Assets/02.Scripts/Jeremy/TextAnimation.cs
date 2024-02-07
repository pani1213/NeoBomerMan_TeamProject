using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    public Text text;
    float coolTime = 0.5f;
    bool isInvisable = true;

    void Update()
    {
        coolTime -= Time.deltaTime;
        if (coolTime <= 0)
        {
            coolTime = 0.5f;
            if (isInvisable)
                text.color = new Color(255f/255f, 255f/255f, 255f/255f, 0f/255f);
            else
                text.color = Color.white;
            isInvisable = !isInvisable;
        }
    }
}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class EndingText : MonoBehaviour
{
    public Text[] TextList;
    public List<string> texts = new List<string>();
    public float Speed = 1f;
    int textIndex = 0;
    bool textLoop = true;

    void Start()
    {
        for (int i = 0; i < TextList.Length; i++)
        {
            texts.Add(string.Empty);
        }
        
    }

    void Update()
    {
        if (textLoop)
        {
            foreach(Text t in TextList)
            {
                Vector2 currentPosition = t.transform.position;
                Vector2 newPosition = currentPosition + Vector2.up * Speed * Time.deltaTime;
                t.transform.position = newPosition;

                if (t.gameObject.transform.localPosition.y > 300)
                {
                    t.gameObject.transform.localPosition = new Vector2(t.gameObject.transform.localPosition.x, -300);
                    t.text = texts[textIndex];

                    if (texts.Count - 1 > textIndex)
                    {
                        textIndex++;
                    }
                    else
                    {
                        textLoop = false;
                    }
                }
            }
        }
    }
}

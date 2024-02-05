using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingText : MonoBehaviour
{
    public Text[] TextList;
    public List<string> texts = new List<string>();
    public float Speed = 20f;

    void Start()
    {
        for (int i = 0; i < TextList.Length; i++)
        {
            if (TextList.Length > 0 && texts.Count > 0)
            {
                TextList[0].text = texts[0];
            }
        }   
    }
    void Update()
    {
        foreach (Text t in TextList)
        {
            Vector2 currentPosition = t.transform.position;
            Vector2 newPosition = currentPosition + Vector2.up * Speed * Time.deltaTime;
            t.transform.position = newPosition;
            
            if (newPosition.y > 0)
            {
                newPosition.y = -100;
            }
        }
    }  
}

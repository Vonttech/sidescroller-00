using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBg : MonoBehaviour
{
    private int xLeftLimit = -1125;
    private RectTransform m_rectTransform;
    private float speed = 150f;

    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(m_rectTransform.transform.localPosition.x > xLeftLimit)
        {
            float moveValue = speed * Time.deltaTime;

            m_rectTransform.transform.localPosition -= Vector3.right * Mathf.RoundToInt(moveValue);
        }
        else
        {
            m_rectTransform.transform.localPosition = Vector3.zero;
        }
    }
}

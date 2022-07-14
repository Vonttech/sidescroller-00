using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conffeti : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_rigidbody;


    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ThrowConffeti()
    {
        float xPosition = Random.Range(-0.5f, 0.5f);
        float yPosition = Random.Range(0.1f, 0.7f);
        m_rigidbody.AddForce(new Vector2(xPosition, yPosition) * 3f, ForceMode2D.Impulse);

    }



}

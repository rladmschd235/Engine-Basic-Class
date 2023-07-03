using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public bool isOveraped = false;
    
    private Renderer myRenderer;
    private Color originColor;

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        originColor = myRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "endPoint")
        {
            myRenderer.material.color = Color.red;
            Debug.Log("충돌");
            isOveraped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "endPoint")
        {
            myRenderer.material.color = originColor;
            Debug.Log("충돌");
            isOveraped = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "endPoint")
        {
            myRenderer.material.color = Color.red;
            Debug.Log("충돌");
            isOveraped = true;
        }
    }
}

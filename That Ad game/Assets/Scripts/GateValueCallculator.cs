using System.Globalization;
using TMPro;
using UnityEngine;

public class GateValueCallculator : MonoBehaviour
{
    public int number;
    public char operation;
    private string gateLabel;

    private Renderer rend;
    private TextMeshPro textMesh ;

    void Start()
    {
        Transform labelTransform = transform.Find("Label");
        textMesh = labelTransform.GetComponent<TextMeshPro>();
        rend = GetComponent<Renderer>();


        gateLabel = operation.ToString() + number.ToString();
        SetColor(Color.blue);

        textMesh.text = gateLabel;
    }

    private void Update()
    {
        gateLabel = operation.ToString() + number.ToString();
        textMesh.text = gateLabel;
    }

    void SetColor(Color color)
    {
        if (rend != null)
        {
            rend.material.color = color;
        }
    }
}

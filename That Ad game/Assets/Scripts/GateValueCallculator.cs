using System.Globalization;
using TMPro;
using UnityEngine;

public class GateValueCallculator : MonoBehaviour
{
    private char[] operations = { '+', '/', '*' };
    public int number;
    private int numberMin = -3;
    private int numberMax = 3;

    public char operation;
    private string gateLabel;

    private Renderer rend;
    private TextMeshPro textMesh ;

    void Start()
    {
        Transform labelTransform = transform.Find("Label");
        textMesh = labelTransform.GetComponent<TextMeshPro>();
        rend = GetComponent<Renderer>();

        operation = operations[Random.Range(0, operations.Length)];
        switch (operation)
        {
            case '+':
                number = Random.Range(numberMin, numberMax + 1);
                if (number > 0)
                {
                    gateLabel = "+" + number.ToString();
                }
                else
                    gateLabel = number.ToString();
                break;
            default:
                number = Random.Range(1, numberMax + 1);
                gateLabel = operation.ToString() + number.ToString();
                break;
        }

        switch (operation)
        {
            case '/':
                SetColor(Color.red);
                break;
            case '*':
                SetColor(Color.blue);
                break;
            default:
                if (number < 0)
                {
                    SetColor(Color.red);
                    break;
                }
                else if (number > 0)
                {
                    SetColor(Color.blue);
                    break;
                }
                SetColor(Color.gray);
                    break;
        }

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

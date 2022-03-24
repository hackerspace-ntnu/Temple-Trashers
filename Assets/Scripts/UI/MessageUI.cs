using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageUI : MonoBehaviour
{
    [SerializeField]
    private GameObject messagePrefab;

    public Color[] colors;

    public enum TextColors
    {
        red,
        green
    }

    public void DisplayMessage(string message, TextColors color)
    {
        TextMeshProUGUI text = Instantiate(messagePrefab, transform.position, messagePrefab.transform.rotation).GetComponentInChildren<TextMeshProUGUI>();
        text.text = message;
        text.color = colors[(int)color];

        Destroy(text, 2.5f);
    }
}

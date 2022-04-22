using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum MessageTextColor
{
    RED,
    GREEN,
}

public class MessageUI : MonoBehaviour
{
    [SerializeField]
    private GameObject messagePrefab = default;

    [SerializeField]
    private Color[] colors = default;

    public void DisplayMessage(string message, MessageTextColor color)
    {
        TextMeshProUGUI text = Instantiate(messagePrefab, transform.position, messagePrefab.transform.rotation).GetComponentInChildren<TextMeshProUGUI>();
        text.text = message;
        text.color = colors[(int)color];

        Destroy(text, 2.5f);
    }
}

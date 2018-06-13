using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class MessageWindow : MonoBehaviour
{

    public Text messageText;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        canvasGroup.DOFade(1, 0.3f);
    }

    public void HideMessage()
    {
        canvasGroup.DOFade(0, 0.3f);
    }
}

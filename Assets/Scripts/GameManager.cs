using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum UnitType { PLAYER, ENEMY, DIVAN }

    public Unit divan;
    public BaseController[] baseControllers;
    public Fountain fountain;
    public MessageWindow messageWindow;
    public CameraController cameraController;
    public Text coinsPanel;

    public bool isGameComplete { get; set; }

    private int timeToStart = 10;
    private int goldCount;

    void Awake()
    {
        Instance = this;
        divan.onDead += GameOver;
    }

    void Start()
    {
        StartCoroutine(Timer());
    }

    public void AddGold(int count){
        goldCount += count;
        coinsPanel.text = string.Format("Gold: {0}", goldCount);
    }

    public void CompleteGame()
    {
        isGameComplete = true;
        cameraController.ActiveCamera(false);
        fountain.Activate(false);
        SelectController.Instance.Activate(false);
        for (int index = 0; index < baseControllers.Length; index++)
        {
            baseControllers[index].DeactiveBaracks();
        }
        messageWindow.ShowMessage("YOU WON!");
    }

    private void GameOver()
    {
        cameraController.ActiveCamera(false);
        divan.gameObject.SetActive(false);
        fountain.Activate(false);
        SelectController.Instance.Activate(false);
        for (int index = 0; index < baseControllers.Length; index++)
        {
            baseControllers[index].DeactiveBaracks();
        }
        messageWindow.ShowMessage("GAME OVER!");
    }

    private IEnumerator Timer()
    {
        for (int second = timeToStart; second > 0; second--)
        {
            messageWindow.ShowMessage(second.ToString());
            yield return new WaitForSeconds(1);
        }
        messageWindow.HideMessage();
        for (int index = 0; index < baseControllers.Length; index++)
        {
            baseControllers[index].ActiveBaracks();
        }
        fountain.Activate(true);
        cameraController.ActiveCamera(true);
        SelectController.Instance.Activate(true);
    }
}

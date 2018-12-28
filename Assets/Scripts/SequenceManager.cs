using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] private List<Selector> selectorsList = new List<Selector>();

    [SerializeField] private GameObject selectors_737_GO;
    [SerializeField] private GameObject selectors_145_GO;
    [SerializeField] private GameObject selectors_190_GO;

    [SerializeField] private GameObject activeSelectorGO;

    [SerializeField] private GameObject cockpit_737_GO;

    [SerializeField] private GameObject PlayerGO;
    public Camera playerCamera;
    [SerializeField] private Transform leftSeatTransform;
    [SerializeField] private Transform centerTransform;
    [SerializeField] private Transform rightSeatTransform;

    [SerializeField] private TextMesh finalText;

    public static SequenceManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartSequenceFlow();
    }

    private void StartSequenceFlow()
    {
        GetActiveSelectorFromPlayerPrefs();
        MovePlayerToSelectedSeat();
        FillSelectorsList();
        //FillSequenceNumbersText();
        SetFirstSelectorActive();
    }

    private void GetActiveSelectorFromPlayerPrefs()
    {
        // ToDO: Change to PlayerPrefs. Temp sentences
        activeSelectorGO = selectors_737_GO;
        cockpit_737_GO.SetActive(true);
    }

    private void MovePlayerToSelectedSeat()
    {
        if (Settings.CurrentSeat == Settings.Seat.Left.ToString())
        {
            PlayerGO.transform.position = leftSeatTransform.position;
        }
        else if (Settings.CurrentSeat == Settings.Seat.Right.ToString())
        {
            PlayerGO.transform.position = rightSeatTransform.position;
        }
        else
        {
            PlayerGO.transform.position = centerTransform.position;
        }
    }

    private void FillSelectorsList()
    {
        foreach (var selector in activeSelectorGO.GetComponentsInChildren<Selector>())
        {
            selectorsList.Add(selector);
        }
    }

    private void FillSequenceNumbersText()
    {
        foreach (var selector in selectorsList)
        {
            if (selector != null)
            {
                selector.gameObject.GetComponentInChildren<TextMesh>().text =
                    (selectorsList.IndexOf(selector) + 1).ToString();
            }
        }
    }

    private void SetFirstSelectorActive()
    {
        foreach (var selector in selectorsList)
        {
            selector.isActive = false;
        }

        selectorsList.First().isActive = true;
    }

    public void MakeNextSelectorActive()
    {
        Destroy(selectorsList.First().gameObject);
        selectorsList.Remove(selectorsList.First());

        if (selectorsList.Count == 0)
        {
            // ToDo: Final message and backToMenu
            Debug.Log("Final!");
            StartCoroutine(FinalRoutine(5));
            return;
        }

        selectorsList.First().isActive = true;
    }

    private IEnumerator FinalRoutine(int delayToRestart)
    {
        finalText.gameObject.SetActive(true);

        while (delayToRestart > 0)
        {
            finalText.text = "All Sequences accomplished successfully!\n<color=#FF4136>" + delayToRestart + "</color>";
            yield return new WaitForSeconds(1f);
            delayToRestart -= 1;
        }

        finalText.text = "Restart!";
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
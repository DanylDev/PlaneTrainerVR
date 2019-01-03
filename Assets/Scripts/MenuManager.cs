using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(LoadDevice("Cardboard"));

        sequenceManager.gameObject.SetActive(false);
        SetCurrentAircraft();
        SetCurrentSeatToggle();
    }

    IEnumerator LoadDevice(string newDevice)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return new WaitForSeconds(0.1f);
        XRSettings.enabled = true;
    }

    #region Aircraft

    [SerializeField] private Image currentAircraftImage;
    [SerializeField] private SequenceManager sequenceManager;

    [Header("Sprites")] [SerializeField] private Sprite[] aircraftSprites = new Sprite[3];
    private int currentAircraftIndex;

    private void SetCurrentAircraft()
    {
        if (Settings.CurrentAircraft == Settings.Aircraft._737.ToString())
        {
            Debug.Log("_737");
            currentAircraftIndex = 0;
            currentAircraftImage.sprite = aircraftSprites[currentAircraftIndex];

            //temp
            buttonStart.gameObject.SetActive(true);
        }
        else if (Settings.CurrentAircraft == Settings.Aircraft._145.ToString())
        {
            Debug.Log("_145");
            currentAircraftIndex = 1;
            currentAircraftImage.sprite = aircraftSprites[currentAircraftIndex];

            //temp
            buttonStart.gameObject.SetActive(false);
        }
        else if (Settings.CurrentAircraft == Settings.Aircraft._190.ToString())
        {
            Debug.Log("_190");
            currentAircraftIndex = 2;
            currentAircraftImage.sprite = aircraftSprites[currentAircraftIndex];

            //temp
            buttonStart.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Check your \"Settings\" static class");
        }
    }

    #endregion

    #region Seat

    [Header("Toggles")] [SerializeField] private Toggle seatLeftToggle;
    [SerializeField] private Toggle seatRightToggle;

    private void SetCurrentSeatToggle()
    {
        if (Settings.CurrentSeat == Settings.Seat.Left.ToString())
        {
            Debug.Log("Left");
            seatLeftToggle.isOn = true;
            seatRightToggle.isOn = false;
        }
        else if (Settings.CurrentSeat == Settings.Seat.Right.ToString())
        {
            Debug.Log("Right");
            seatRightToggle.isOn = true;
            seatLeftToggle.isOn = false;
        }
        else
        {
            Debug.Log("Check your \"Settings\" static class");
        }
    }

    #endregion

    #region Buttons

    private Coroutine gazingAtButtonCoroutine;

    [Header("Buttons")] [SerializeField] private Button buttonPreviousAircraft;
    [SerializeField] private Button buttonNextAircraft;
    [SerializeField] private Button buttonStart;


    public void ExecuteRelatedMethod(GameObject currentGO)
    {
        gazingAtButtonCoroutine = StartCoroutine(ExecutionRoutine(currentGO, 1f));
    }

    private IEnumerator ExecutionRoutine(GameObject selectedGO, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (selectedGO == buttonPreviousAircraft.gameObject)
        {
            SwitchAircraft(-1);
        }

        if (selectedGO == buttonNextAircraft.gameObject)
        {
            SwitchAircraft(+1);
        }

        if (selectedGO == seatLeftToggle.gameObject)
        {
            ToggleSeat(false);
        }

        if (selectedGO == seatRightToggle.gameObject)
        {
            ToggleSeat(true);
        }

        if (selectedGO == buttonStart.gameObject)
        {
            StartGameplay();
        }
    }

    private void SwitchAircraft(int _crementValue)
    {
        currentAircraftIndex += _crementValue;
        if (currentAircraftIndex > 2)
        {
            currentAircraftIndex = 0;
        }

        if (currentAircraftIndex < 0)
        {
            currentAircraftIndex = 2;
        }

        switch (currentAircraftIndex)
        {
            case 0:
                Settings.CurrentAircraft = Settings.Aircraft._737.ToString();
                SetCurrentAircraft();
                break;
            case 1:
                Settings.CurrentAircraft = Settings.Aircraft._145.ToString();
                SetCurrentAircraft();
                break;
            case 2:
                Settings.CurrentAircraft = Settings.Aircraft._190.ToString();
                SetCurrentAircraft();
                break;
            default:
                Debug.Log("Check currentAircraftIndex value");
                break;
        }
    }

    private void ToggleSeat(bool isRight)
    {
        Settings.CurrentSeat = isRight ? Settings.Seat.Right.ToString() : Settings.Seat.Left.ToString();

        SetCurrentSeatToggle();
    }

    private void StartGameplay()
    {
        sequenceManager.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ResetGazingCoroutine()
    {
        if (gazingAtButtonCoroutine != null)
        {
            StopCoroutine(gazingAtButtonCoroutine);
        }
    }

    #endregion
}
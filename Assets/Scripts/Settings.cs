using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for various Options Data control using PlayerPrefs
/// </summary>
public static class Settings
{
    #region Aircraft

    private const string currentAircraftKey = "Aircraft";

    public enum Aircraft
    {
        _737,
        _145,
        _190,
    }

    public static string CurrentAircraft
    {
        get { return RegGetCurrentAircraft(); }
        set { RegSetCurrentAircraft(value); }
    }

    private static string RegGetCurrentAircraft()
    {
        if (PlayerPrefs.GetString(currentAircraftKey) == string.Empty ||
            PlayerPrefs.GetString(currentAircraftKey) == null)
        {
            //Default Value
            RegSetCurrentAircraft(Aircraft._737.ToString());
        }

        return PlayerPrefs.GetString(currentAircraftKey);
    }

    private static void RegSetCurrentAircraft(string value)
    {
        PlayerPrefs.SetString(currentAircraftKey, value);
    }

    #endregion

    #region Seat

    private const string currentSeatKey = "Seat";

    public enum Seat
    {
        Left,
        Right
    }

    public static string CurrentSeat
    {
        get { return RegGetCurrentSeat(); }
        set { RegSetCurrentSeat(value); }
    }

    private static string RegGetCurrentSeat()
    {
        if (PlayerPrefs.GetString(currentSeatKey) == string.Empty || PlayerPrefs.GetString(currentSeatKey) == null)
        {
            //Default Value
            RegSetCurrentSeat(Seat.Left.ToString());
        }

        return PlayerPrefs.GetString(currentSeatKey);
    }

    private static void RegSetCurrentSeat(string value)
    {
        PlayerPrefs.SetString(currentSeatKey, value);
    }

    #endregion
}
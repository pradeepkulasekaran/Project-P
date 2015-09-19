﻿using UnityEngine;
using System.Collections;
using Prime31;

public class NotifMgr : MonoBehaviour {

	private int _tenSecondNotificationId;
    private int _oneDayNotificationId;

    long SecInDay = 24 * 60 * 60;

    void OnEnable()
    {
        EtceteraAndroidManager.notificationReceivedEvent += notificationReceivedEvent;
    }


    void OnDisable()
    {
        EtceteraAndroidManager.notificationReceivedEvent -= notificationReceivedEvent;
    }


    void Start()
    {
        ChkForNotif();
    }


    void Update()
    {
    }

    
    public void SetNotif_10Sec()
    {
        var noteConfig = new AndroidNotificationConfiguration(10, "Got an energy", "Play the epic clash", "havefun")
        {
            extraData = "ten-second-note",
            groupKey = "my-note-group",
            smallIcon = "app_icon"            
        };

        // turn off sound and vibration for this notification
        noteConfig.sound = false;
        noteConfig.vibrate = false;

        _tenSecondNotificationId = EtceteraAndroid.scheduleNotification(noteConfig);
    }


    public void SetNotif_24Hr()
    {
        var noteConfig = new AndroidNotificationConfiguration(SecInDay, "Collect your daily bonus", "Play the epic clash", "havefun")
        {
            extraData = "one-day-note",
            groupKey = "my-note-group"            
        };
        _oneDayNotificationId = EtceteraAndroid.scheduleNotification(noteConfig);
    }


    public void CancelAllNotif()
    {
        EtceteraAndroid.cancelNotification( _tenSecondNotificationId );
        EtceteraAndroid.cancelNotification( _oneDayNotificationId );
        EtceteraAndroid.cancelAllNotifications();
    }


    void notificationReceivedEvent(string extraData)
    {
        Debug.Log("notificationReceivedEvent: " + extraData);
    }


    public void EnergyRefillCheck()
    {
        /*
        System.DateTime currentDate = System.DateTime.Now;
        long oldDateLong = SavedData.Inst.GetLastBonusTime();
        System.DateTime oldDate = System.DateTime.FromBinary(oldDateLong);

        System.TimeSpan diff = currentDate.Subtract(oldDate);

        if (diff.TotalSeconds >= 10)
        {
            Debug.Log("BONUS BONUS BONUS");
            GiveDailyBonus();
        }
        else
        {
            Debug.Log("BONUS After : " + (SecInDay - diff.TotalSeconds) + " Seconds");
        }
        */
    }


    public void DailyBonusCheck()
    {
        System.DateTime currentDate = System.DateTime.Now;
        long oldDateLong = SavedData.Inst.GetLastBonusTime();
        System.DateTime oldDate = System.DateTime.FromBinary(oldDateLong);

        System.TimeSpan diff = currentDate.Subtract(oldDate);

        if (diff.TotalSeconds >= SecInDay)
        {
            Debug.Log("BONUS BONUS BONUS");
            GiveDailyBonus();
        }
        else
        {
            Debug.Log("BONUS After : " + (SecInDay - diff.TotalSeconds) + " Seconds");
        }
    }


    public void GiveDailyBonus()
    {
        // save it to pref
        SavedData.Inst.OnBonusGiven();
    }


    public void ChkForNotif()
    {
        EtceteraAndroid.checkForNotifications();
        Debug.Log("checkForNotifications");
    }
}

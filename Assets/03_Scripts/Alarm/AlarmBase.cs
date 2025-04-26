using System.Collections.Generic;

namespace _03_Scripts.Alarm
{
    public class AlarmBase
    {
        
        public string alarm24Time;
        public int alarm24Hour;
        public int alarm24Minute;

        public string alarm12time; /// <summary>
                                   ///  tt hh:mm
                                   /// </summary>
        public string alarm12tt;
        public string alarm12HH;
        public string alarm12mm;
        
        public bool isBellOn;
        
        public bool isAUTOQuitOn;
        public int AUTOQuitMinutes;
        
        public Dictionary<string, bool> alarmRepeatDays;
        //생성자에서 초기화 시도했으나 다른 클래스에서 사용실패.
        /*
        public AlarmBase()
        {
            alarmRepeatDays["Monday"] = false;
            alarmRepeatDays["Tuesday"] = false;
            alarmRepeatDays["Wednesday"] = false;
            alarmRepeatDays["Thursday"] = false;
            alarmRepeatDays["Friday"] = false;
            alarmRepeatDays["Saturday"] = false;
            alarmRepeatDays["Sunday"] = false;
        }
        */
    }
}

using System;

namespace TimeAndTimePeroidLib
{
    #region Time
    /// <summary>
    /// Struct Time with implemented interfaces: IEquatable, IComparable
    /// </summary>
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        /// <summary>
        /// Added variables
        /// <param name="Hours"></param>
        /// <param name="Minutes"></param>
        /// <param name="Seconds"></param>
        /// </summary>
        public byte Hours { get; private set; }

        public byte Minutes { get; private set; }

        public byte Seconds { get; private set; }

        /// <summary>
        /// Added constructor with exception
        /// </summary>
        /// <param name="h"></param>
        /// <param name="m"></param>
        /// <param name="s"></param>
        /// <exception cref="ArgumentException"> Throw exception if time data is incorrect </exception>
        public Time(byte h, byte m = 0, byte s = 0)
        {
            if (h > 23 || h < 0 || m > 59 || m < 0 || s > 59 || s < 0)
                throw new ArgumentException("Wrong time data");


            this.Hours = h;
            this.Minutes = m;
            this.Seconds = s;
        }
        /// <summary>
        /// Added converter: time -> seconds
        /// </summary>
        /// <param name="time"></param>
        /// <returns> Time in seconds </returns>
        private static long ConvertTimeToSeconds(Time time) => time.Hours * 3600 + time.Minutes * 60 + time.Seconds;

        /// <summary>
        /// Added constructor for string
        /// </summary>
        /// <param name="time"></param>
        public Time(string time)
        {
            var times = time.Split(':');

            Hours = Convert.ToByte(times[0]);
            Minutes = Convert.ToByte(times[1]);
            Seconds = Convert.ToByte(times[2]);
        }

        /// <summary>
        /// Added override for ToString()
        /// </summary>
        /// <returns> Time representation: HH:MM:SS </returns>
        public override string ToString()
        {
            return String.Format("{0:00}:{1:00}:{2:00}",this.Hours, this.Minutes, this.Seconds);
        }

        /// <summary>
        /// Added check if times are equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns> True if they are equal, false if not </returns>
        public bool Equals(Time other)
        {
            return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
        }

        /// <summary>
        /// Override Equals
        /// </summary>
        /// <param name="obj"></param>
        public override bool Equals(object obj)
        {
            return obj is Time other && Equals(other);
        }

        /// <summary>
        /// Added compare for two times
        /// </summary>
        /// <param name="other"></param>
        public int CompareTo(Time other)
        {
            if (Hours - other.Hours != 0) return Hours - other.Hours;
            if (Minutes - other.Minutes != 0) return Minutes - other.Minutes;
            if (Seconds - other.Seconds != 0) return Seconds - other.Seconds;
            return 0;
        }
        /// <summary>
        /// Added operator ==
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator ==(Time a, Time b) => a.Equals(b);
        /// <summary>
        /// Added operator !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator !=(Time a, Time b) => !(a == b);
        /// <summary>
        /// Added operator <
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator <(Time a, Time b) => a.CompareTo(b) < 0;
        /// <summary>
        /// Added operator <=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator <=(Time a, Time b) => a.CompareTo(b) <= 0;
        /// <summary>
        /// Added operator >
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator >(Time a, Time b) => a.CompareTo(b) > 0;
        /// <summary>
        /// Added operator >=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator >=(Time a, Time b) => a.CompareTo(b) >= 0;
        /// <summary>
        /// Added operator +
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static Time operator +(Time a, TimePeriod b) => a.Plus(b);

        /// <summary>
        /// Added method that allows  for operation on time (modulo)
        /// </summary>
        /// <param name="timeperiod"></param>
        public Time Plus(TimePeriod timeperiod)
        {
            var fullSeconds = Hours * 3600 + Minutes * 60 + Seconds + timeperiod.Seconds;

            var hours = (byte)(fullSeconds / 3600 > 23 ? fullSeconds / 3600 % 24 : fullSeconds / 3600);
            var minutes = (byte)(fullSeconds / 60 % 60);
            var seconds = (byte)(fullSeconds % 60);

            return new Time(hours, minutes, seconds);
        }

        /// <summary>
        /// Added static method that allows for operation on time & timeperoid (modulo)
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeperiod"></param>
        public static Time Plus(Time time, TimePeriod timeperiod)
        {
            var fullSeconds = ConvertTimeToSeconds(time) + timeperiod.Seconds;

            var hours = (byte)(fullSeconds / 3600 > 23 ? fullSeconds / 3600 % 24 : fullSeconds / 3600);
            var minutes = (byte)(fullSeconds / 60 % 60);
            var seconds = (byte)(fullSeconds % 60);

            return new Time(hours, minutes, seconds);
        }

        /// <summary>
        /// Added override for GetHashCode()
        /// </summary>
        public override int GetHashCode() => Seconds.GetHashCode();

    }

    #endregion


    #region TimePeriod
    /// <summary>
    /// Struct TimePeriod with interfaces: IEquatable, IComparable
    /// </summary>
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        /// <summary>
        /// Added variable
        /// <param name="Seconds"></param>
        /// </summary>
        public long Seconds { get; private set; }

        /// <summary>
        /// Added constructor with exception
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <exception cref="ArgumentException"> Throw exception if time data is incorrect </exception>
        public TimePeriod(byte hour, byte minutes, byte seconds = 0)
        {
            if (hour <= 0 || minutes > 59 || minutes < 0 || seconds > 59 || seconds < 0)
                throw new ArgumentException("Wrong time data");

            else this.Seconds = hour * 3600 + minutes * 60 + seconds;

        }

        /// <summary>
        /// Added constructor
        /// </summary>
        /// <param name="seconds"></param>
        public TimePeriod(long seconds)
        {
            this.Seconds = seconds;
        }

        /// <summary>
        /// Added converter: time -> seconds
        /// </summary>
        /// <param name="time"></param>
        /// <returns> Time in seconds </returns>
        private static long ConvertTimeToSeconds(Time time)
        {
            return time.Hours * 3600 + time.Minutes * 60 + time.Seconds;

        }

        /// <summary>
        /// Added constructor for string
        /// </summary>
        /// <param name="timeperiod"></param>
        public TimePeriod(string timeperiod)
        {
            var time = timeperiod.Split(':');

            var hour = Convert.ToInt64(time[0]) >= 0 ? Convert.ToInt64(time[0]) : throw new ArgumentException();
            var minute = Convert.ToInt64(time[1]) < 60 ? Convert.ToInt64(time[1]) : throw new ArgumentException();
            var second = Convert.ToInt64(time[2]) < 60 ? Convert.ToInt64(time[2]) : throw new ArgumentException();
            Seconds = hour * 3600 + minute * 60 + second;
        }

        /// <summary>
        /// Added difference converter for 2 times to seconds
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        public TimePeriod(Time t1, Time t2)
        {
            var firstTime = ConvertTimeToSeconds(t1);
            var secondTime = ConvertTimeToSeconds(t2);

            Seconds = secondTime - firstTime < 0 ? (secondTime - firstTime) + 24 * 3600 : secondTime - firstTime;
        }


        /// <summary>
        /// Added override for ToString()
        /// </summary>
        /// <returns> Time representation: HH:MM:SS </returns>
        public override string ToString()=>  $"{Seconds / 3600}:{(Seconds / 60) % 60:00}:{Seconds % 60:00}";

        /// <summary>
        /// Added check if time periods are equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns> True if they are equal, false if not </returns>
        public bool Equals(TimePeriod other) => Seconds == other.Seconds;

        /// <summary>
        /// Override Equals
        /// </summary>
        /// <param name="obj"></param>
        public override bool Equals(object obj) => obj is TimePeriod other && Equals(other);

        /// <summary>
        /// Added compare for two time periods
        /// </summary>
        /// <param name="other"></param>
        public int CompareTo(TimePeriod other) => (int)(Seconds - other.Seconds);

        /// <summary>
        /// Added operator ==
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator ==(TimePeriod a, TimePeriod b) => a.Equals(b);
        /// <summary>
        /// Added operator !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator !=(TimePeriod a, TimePeriod b) => !(a == b);
        /// <summary>
        /// Added operator <
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator <(TimePeriod a, TimePeriod b) => a.CompareTo(b) < 0;
        /// <summary>
        /// Added operator <=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator <=(TimePeriod a, TimePeriod b) => a.CompareTo(b) <= 0;
        /// <summary>
        /// Added operator >
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(TimePeriod a, TimePeriod b) => a.CompareTo(b) > 0;
        /// <summary>
        /// Added operator >=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator >=(TimePeriod a, TimePeriod b) => a.CompareTo(b) >= 0;
        /// <summary>
        /// Added operator +
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static TimePeriod operator +(TimePeriod a, TimePeriod b) => a.Plus(b);

        /// <summary>
        /// Added method that allows for operation on time peroid
        /// </summary>
        /// <param name="timeperiod"></param>
        public TimePeriod Plus(TimePeriod timeperiod) => new TimePeriod(Seconds + timeperiod.Seconds);

        /// <summary>
        /// Added static method that allows for operation on two time peroids
        /// </summary>
        /// <param name="timeperiod1"></param>
        /// <param name="timeperiod2"></param>
        public static TimePeriod Plus(TimePeriod timeperiod1, TimePeriod timeperiod2) => new TimePeriod(timeperiod1.Seconds + timeperiod2.Seconds);

        /// <summary>
        /// Added override for GetHashCode()
        /// </summary>
        public override int GetHashCode() => Seconds.GetHashCode();
        
    }
    #endregion 

}


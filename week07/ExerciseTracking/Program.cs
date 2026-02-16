
using System;
using System.Collections.Generic;

abstract class Activity
{
    private DateTime _date;
    private int _minutes;

    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public DateTime Date { get { return _date; } }
    public int Minutes { get { return _minutes; } }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        string activityType = this.GetType().Name.Replace("Activity", "");
        double distance = GetDistance();
        double speed = GetSpeed();
        double pace = GetPace();
        return $"{Date:dd MMM yyyy} {activityType} ({Minutes} min): Distance {distance:F1} km, Speed: {speed:F1} kph, Pace: {pace:F2} min per km";
    }
}

class RunningActivity : Activity
{
    private double _distance;

    public RunningActivity(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Minutes) * 60;
    }

    public override double GetPace()
    {
        return Minutes / GetDistance();
    }
}

class CyclingActivity : Activity
{
    private double _speed;

    public CyclingActivity(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetDistance()
    {
        return (GetSpeed() / 60) * Minutes;
    }

    public override double GetPace()
    {
        return 60 / GetSpeed();
    }
}

class SwimmingActivity : Activity
{
    private int _laps;

    public SwimmingActivity(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        return _laps * 0.05; // 50 meters = 0.05 km
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Minutes) * 60;
    }

    public override double GetPace()
    {
        return Minutes / GetDistance();
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new RunningActivity(new DateTime(2022, 11, 3), 30, 4.8),
            new CyclingActivity(new DateTime(2022, 11, 4), 30, 9.7),
            new SwimmingActivity(new DateTime(2022, 11, 5), 30, 20) // Example: 20 laps = 1 km
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

using System;

namespace UI.Models
{
    public class GameSettingsModel
    {
        public string Book { get; set; } = "-ALL-";
        public string Chapter { get; set; } = "-ALL-";
        public string FormClass { get; set; } = "-ALL-";
        public double PointsToPass { get;  set; } = 1;
        public double PointsPerGoodAnwser { get;  set; } = 0.5;
        public double PointsPerBadAnwser { get;  set; } = 0.5;
        public bool EnableTimeChallange { get;  set; } = false;
        public TimeSpan TimePerQuestion { get;  set; } = new TimeSpan(0, 0, 0);
        public TimeSpan TimePerGame { get;  set; } = new TimeSpan(0, 0, 0);
    }
}

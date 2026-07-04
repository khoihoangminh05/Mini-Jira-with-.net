namespace PersonalTaskManager.Web.Helpers
{
    public static class ReportHelper
    {
        /// <summary>Màu progress bar Bootstrap theo % hoàn thành.</summary>
        public static string GetProgressBarClass(int percent)
        {
            if (percent >= 100)
            {
                return "bg-success";
            }

            if (percent >= 50)
            {
                return "bg-primary";
            }

            if (percent > 0)
            {
                return "bg-warning text-dark";
            }

            return "bg-secondary";
        }

        public static string FormatSummaryText(int done, int total, int percent)
        {
            return string.Format("{0}/{1} hoàn thành ({2}%)", done, total, percent);
        }
    }
}

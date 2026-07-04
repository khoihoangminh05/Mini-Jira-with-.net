using System;

namespace PersonalTaskManager.Core.Entities
{
    public class ActivityLog
    {
        public int LogId { get; set; }

        public int UserId { get; set; }

        public int? ProjectId { get; set; }

        public int? TaskId { get; set; }

        public string ActionType { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

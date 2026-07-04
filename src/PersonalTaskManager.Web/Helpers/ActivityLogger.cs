using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Infrastructure.Repositories;

namespace PersonalTaskManager.Web.Helpers
{
    /// <summary>Ghi activity log — bỏ qua lỗi nếu bảng ACTIVITY_LOG chưa migrate.</summary>
    public static class ActivityLogger
    {
        private static readonly IActivityLogRepository Repository = new ActivityLogRepository();

        public static void TryLog(int userId, int? projectId, int? taskId, string actionType, string message)
        {
            try
            {
                Repository.Add(userId, projectId, taskId, actionType, message);
            }
            catch
            {
                // Bảng ACTIVITY_LOG chưa tạo — không làm crash app
            }
        }
    }
}

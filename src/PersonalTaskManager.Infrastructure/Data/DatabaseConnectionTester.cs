using System;

namespace PersonalTaskManager.Infrastructure.Data
{
    /// <summary>Kiểm tra kết nối Oracle — dùng từ Home/About ở Phase 1.</summary>
    public static class DatabaseConnectionTester
    {
        public static bool TryConnect(out string message)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    // Mở connection để xác minh Oracle + connection string
                    var connection = context.Database.Connection;
                    connection.Open();
                    connection.Close();

                    message = "Kết nối Oracle thành công.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = "Lỗi kết nối: " + ex.Message;
                return false;
            }
        }
    }
}

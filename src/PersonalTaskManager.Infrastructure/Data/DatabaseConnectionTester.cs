using System;
using System.Linq;

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
                    // Mở connection + thử query bảng USERS (bắt lỗi schema dbo, thiếu bảng, ...)
                    var connection = context.Database.Connection;
                    connection.Open();
                    context.Users.Take(1).ToList();
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

using System;
using System.ComponentModel.DataAnnotations;
using PersonalTaskManager.Core.Enums;

namespace PersonalTaskManager.Web.Models.Task
{
    public class TaskFormViewModel
    {
        public int? TaskId { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề task.")]
        [Display(Name = "Tiêu đề")]
        [StringLength(200, ErrorMessage = "Tiêu đề tối đa 200 ký tự.")]
        public string Title { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn mức ưu tiên.")]
        [Display(Name = "Ưu tiên")]
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        [Required(ErrorMessage = "Vui lòng chọn trạng thái.")]
        [Display(Name = "Trạng thái")]
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;

        [Display(Name = "Hạn chót")]
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PersonalTaskManager.Web.Models.Project
{
    public class ProjectFormViewModel
    {
        public int? ProjectId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên dự án.")]
        [Display(Name = "Tên dự án")]
        [StringLength(200, ErrorMessage = "Tên dự án tối đa 200 ký tự.")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        [StringLength(2000, ErrorMessage = "Mô tả tối đa 2000 ký tự.")]
        public string Description { get; set; }
    }
}

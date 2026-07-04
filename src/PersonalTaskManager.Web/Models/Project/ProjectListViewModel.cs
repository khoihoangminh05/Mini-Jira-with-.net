using System;
using System.Collections.Generic;

namespace PersonalTaskManager.Web.Models.Project
{
    public class ProjectListViewModel
    {
        public IList<ProjectListItemViewModel> Projects { get; set; } = new List<ProjectListItemViewModel>();
    }

    public class ProjectListItemViewModel
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

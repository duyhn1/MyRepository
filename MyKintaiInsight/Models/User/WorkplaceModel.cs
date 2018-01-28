using System;
namespace MyKintaiInsight.Models.User
{
    public class WorkplaceModel
    {
        public WorkplaceModel()
        {
        }
        public int employeeId { get; set; }
        public string customerCode { get; set; }
        public decimal longitute { get; set; }
        public decimal latitude { get; set; }
        public int id { get; set; }
        public DateTime? createdAt { get; set; }
        public string createdBy { get; set; }
        public DateTime? modifiedAt { get; set; }
        public string modifiedBy { get; set; }
        public bool isDeleted { get; set; }
    }
}

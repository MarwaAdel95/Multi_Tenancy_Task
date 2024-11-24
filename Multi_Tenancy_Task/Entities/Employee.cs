namespace Multi_Tenancy_Task.Entities
{
    public class Employee:EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Department Department { get; set; }
        public Tenant Tenant { get; set; }
        public int DepartmentId { get; set; }
    }
}

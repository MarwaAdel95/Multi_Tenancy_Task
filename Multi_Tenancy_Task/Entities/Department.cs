namespace Multi_Tenancy_Task.Entities
{
    public class Department:EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}

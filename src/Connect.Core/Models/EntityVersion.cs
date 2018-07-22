namespace Connect.Core.Models
{
    public class EntityVersion
    {
        public System.Guid EntityVersionId { get; set; }
        public System.Guid EntityId { get; set; }
        public System.Guid Version { get; set; }
        public string EntityName { get; set; }
    }
}

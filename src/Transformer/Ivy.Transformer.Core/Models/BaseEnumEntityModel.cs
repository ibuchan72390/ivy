using Ivy.Data.Core.Interfaces.Domain;

namespace Ivy.Transformer.Core.Models
{
    public class BaseEnumEntityModel : BaseEntityModel, IEnumEntityProperties
    {
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public int SortOrder { get; set; }
    }
}

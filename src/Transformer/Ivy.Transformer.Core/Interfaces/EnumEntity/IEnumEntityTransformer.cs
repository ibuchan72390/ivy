using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Transformer.Core.Models;

namespace Ivy.Transformer.Core.Interfaces.EnumEntity
{
    public interface IEnumEntityTransformer<TEnumEntity, TViewModel> : 
        IEnumEntityToViewModelTransformer<TEnumEntity, TViewModel>,
        IEnumEntityToViewModelListTransformer<TEnumEntity, TViewModel>
        where TEnumEntity : IEntity, IEnumEntityProperties
        where TViewModel : BaseEnumEntityModel
    {
    }
}

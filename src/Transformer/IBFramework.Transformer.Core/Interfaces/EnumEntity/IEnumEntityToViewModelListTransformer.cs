using IBFramework.Data.Core.Interfaces.Domain;
using IBFramework.Transformer.Core.Interfaces.Entity;
using IBFramework.Transformer.Core.Models;

namespace IBFramework.Transformer.Core.Interfaces.EnumEntity
{
    public interface IEnumEntityToViewModelListTransformer<TEnumEntity, TViewModel> :
        IEntityToViewModelListTransformer<TEnumEntity, TViewModel>
        where TEnumEntity : IEntity, IEnumEntityProperties
        where TViewModel : BaseEnumEntityModel
    {
    }
}

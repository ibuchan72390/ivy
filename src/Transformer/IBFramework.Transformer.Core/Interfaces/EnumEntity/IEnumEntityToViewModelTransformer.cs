using IBFramework.Data.Core.Interfaces.Domain;
using IBFramework.Transformer.Core.Interfaces.Entity;
using IBFramework.Transformer.Core.Models;

namespace IBFramework.Transformer.Core.Interfaces.EnumEntity
{
    /*
     * Is there some way to remove the TEnum from this interface generic properites???
     * It doesn't really seem to be needed in any way, we're simply converting the base
     */

    public interface IEnumEntityToViewModelTransformer<TEnumEntity, TViewModel> :
        IEntityToViewModelTransformer<TEnumEntity, TViewModel>
        where TEnumEntity: IEntity, IEnumEntityProperties
        where TViewModel: BaseEnumEntityModel
    {
    }
}

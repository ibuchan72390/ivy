using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Transformer.Base.Entity;
using Ivy.Transformer.Core.Interfaces.EnumEntity;
using Ivy.Transformer.Core.Models;

namespace Ivy.Transformer.Base
{
    public class BaseEnumEntityTransformer<TEnumEntity, TEnumModel> : 
        BaseEntityTransformer<TEnumEntity, TEnumModel>, 
        IEnumEntityTransformer<TEnumEntity, TEnumModel>

        where TEnumEntity : IEnumEntityProperties, IEntity, new()
        where TEnumModel : BaseEnumEntityModel, new()
    {
        public override TEnumModel Transform(TEnumEntity entity)
        {
            var model = base.Transform(entity);

            if (model == null) return null;

            model.Name = entity.Name;
            model.FriendlyName = entity.FriendlyName;

            return model;
        }

        public override TEnumEntity Transform(TEnumModel model)
        {
            var entity = base.Transform(model);

            if (model == null) return default(TEnumEntity);

            entity.Name = model.Name;
            entity.FriendlyName = model.FriendlyName;

            return entity;
        }
    }
}

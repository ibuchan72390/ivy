﻿using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Transformer.Core.Interfaces.Entity;
using Ivy.Transformer.Core.Models;

namespace Ivy.Transformer.Core.Interfaces.EnumEntity
{
    public interface IEnumEntityToViewModelListTransformer<TEnumEntity, TViewModel> :
        IEntityToViewModelListTransformer<TEnumEntity, TViewModel>
        where TEnumEntity : IEntity, IEnumEntityProperties
        where TViewModel : BaseEnumEntityModel
    {
    }
}
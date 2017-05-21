using IBFramework.Data.Core.Interfaces.Domain;
using System.Collections.Generic;

namespace IBFramework.Data.Core.Domain
{
    public class BaseEntityWithReferences : IEntityWithReferences
    {
        #region Variables & Constants

        public Dictionary<string, object> References { get; set; }

        #endregion
    }
}

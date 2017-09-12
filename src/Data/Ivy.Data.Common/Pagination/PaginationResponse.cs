using Ivy.Data.Core.Interfaces.Pagination;
using System.Collections.Generic;

namespace Ivy.Data.Common.Pagination
{
    public class PaginationResponse<T> : IPaginationResponse<T>
    {
        #region Constructor

        public PaginationResponse()
        {
            Data = new List<T>();
        }

        #endregion

        #region Public Methods

        public IEnumerable<T> Data { get; set; }

        public int TotalCount { get; set; }

        #endregion
    }
}

using Ivy.Data.Core.Interfaces.Domain;
using System.Collections.Generic;
using System.Linq;

/*
 * This lives in Core as to prevent forcing consumers to reference the implementation project.
 * 
 * It's standard to reference the Core in other projects, just makes good sense to reference the interfaces,
 * then we let the IoC container determine the implementation at runtime.
 * 
 * As such, since we reference the interfaces anyway, it makes sense for these implementation to exist here.
 * Implementations should exist in Core for PURE FUNCTIONS ONLY
 */

namespace Ivy.Utility.Core.Extensions
{
    public static class IntersectAndExcludeExtensions
    {
        public static IntersectExcludeResult<T> IntersectAndExcludeValues<T>(this IEnumerable<T> existing, IEnumerable<T> incoming)
        {
            return new IntersectExcludeResult<T>
            {
                ExistingOnly = existing.Except(incoming),
                ExistingOverlapping = existing.Intersect(incoming),
                IncomingOverlapping = incoming.Intersect(existing),
                IncomingOnly = incoming.Except(existing)
            };
        }

        public static IntersectExcludeResult<T> IntersectAndExcludeEntities<T>(this IEnumerable<T> existing, IEnumerable<T> incoming)
            where T : IEntity
        {
            var existingIds = existing.Select(x => x.Id);
            var incomingIds = incoming.Select(x => x.Id);

            return new IntersectExcludeResult<T>
            {
                ExistingOnly = existing.Where(x => !incomingIds.Contains(x.Id)),
                ExistingOverlapping = existing.Where(x => incomingIds.Contains(x.Id)),
                IncomingOverlapping = incoming.Where(x => existingIds.Contains(x.Id)),
                IncomingOnly = incoming.Where(x => !existingIds.Contains(x.Id))
            };
        }
    }

    public class IntersectExcludeResult<T>
    {
        public IEnumerable<T> ExistingOnly { get; set; }
        public IEnumerable<T> ExistingOverlapping { get; set; }
        public IEnumerable<T> IncomingOverlapping { get; set; }
        public IEnumerable<T> IncomingOnly { get; set; }
    }
}

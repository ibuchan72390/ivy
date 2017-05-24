using Ivy.Data.Common.Attributes;
using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Data.Core.Interfaces.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ivy.Data.Common.Sql
{
    public class SqlPropertyGenerator : ISqlPropertyGenerator
    {
        public IList<string> GetSqlPropertyNames<T>()
        {
            var _entityType = typeof(T);

            IList<string> results = new List<string>();

            var memberNames = _entityType.GetProperties();

            var viableMemberNames = memberNames
                .Where(IsNotIgnoredAttribute)
                .Where(IsNotCollection);

            var stringMembers = viableMemberNames.Where(IsNotEntity);
            results = stringMembers.Select(x => x.Name).ToList();

            var idMembers = viableMemberNames.Where(IsEntity);
            results = results.Concat(idMembers.Select(x => $"{x.Name}Id")).ToList();

            return results;
        }

        private bool IsEntity(PropertyInfo property)
        {
            return TestPropertyContainsInterface(property, typeof(IEntityWithTypedId<>));
        }

        private bool IsNotEntity(PropertyInfo property)
        {
            return !IsEntity(property);
        }

        private bool IsNotIgnoredAttribute(PropertyInfo property)
        {
            return !property.CustomAttributes
                .Select(y => y.AttributeType)
                .Contains(typeof(IgnoreAttribute));
        }

        private bool IsNotCollection(PropertyInfo property)
        {
            // Do IEnumerable<> instead of IEnumerable, the latter includes string
            // still not working because string is an IEnumerable<char>

            // Maybe ?? 
            //return !property.PropertyType.IsArray && property.PropertyType != typeof(string);

            // Seems to do the trick, but may not be a sufficient test
            return !TestPropertyContainsInterface(property, typeof(ICollection<>));
        }

        private bool TestPropertyContainsInterface(PropertyInfo property, Type testType)
        {
            var interfaceNames = property.PropertyType
                .GetInterfaces().Select(x => x.FullName);

            // FullInterfaceNames guarantees we get full namespace matching
            // The generic type declaration will be after the FullName type declaration
            return interfaceNames.Any(x => x.Contains(testType.FullName));

            // This doesn't seem to work due to the generic
            // Let's just compare the name to make sure it's a dead match
            // should come out to something like IEntityWithTypedId'1
            //return interfaces.Contains(typeof(IEntityWithTypedId<>));
        }
    }
}

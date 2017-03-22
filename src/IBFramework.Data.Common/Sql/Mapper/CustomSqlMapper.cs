//using IBFramework.Core.Data.Domain;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using Dapper;
//using Dapper.FluentMap.Conventions;

///*
// * We're going to need to find an alternate way to test this
// * We're probably going to want to fire this before the IoC generation
// * That way we can run these concurrently and allow startup to occur as quickly as possible
// */


//namespace IBFramework.Data.Common.Sql.Mapper
//{
   
//    public static class CustomSqlMapper
//    {
//        #region Variables & Constants

//        public static bool IsInitialized { get; }

//        public class ForeignKeyConvention : Convention
//        {
//            public ForeignKeyConvention()
//            {

//                /*
//                 * This doesn't appear to be working the way I want it to
//                 */

//                //Properties<IEntityReferences>()
//                //    .Where(c => c.Name.ToLower().Substring(c.Name.Length - 2, 2) == "id")
//                //    .Configure(c => c.Transform());
//            }
//        }


//        #endregion

//        #region Public Methods

//        public static void InitializeTypeMaps()
//        {
//            var currentAssemblies = GetAssemblies();

//            var remapTypes = new List<Type>();

//            foreach (var assembly in currentAssemblies)
//            {
//                foreach (var classRecord in assembly.GetTypes())
//                {
//                    if (classRecord.GetInterfaces().Contains(typeof(IEntityReferences)))
//                    {
//                        remapTypes.Add(classRecord);
//                    }
//                }
//            }

//            foreach (var remapType in remapTypes)
//            {
//                var oldMap = SqlMapper.GetTypeMap(remapType);
//            }
//        }

//        #endregion

//        #region Helper Methods

//        public static IEnumerable<Assembly> GetAssemblies()
//        {
//            var list = new List<string>();
//            var stack = new Stack<Assembly>();

//            stack.Push(Assembly.GetEntryAssembly());

//            do
//            {
//                var asm = stack.Pop();

//                yield return asm;

//                foreach (var reference in asm.GetReferencedAssemblies())
//                    if (!list.Contains(reference.FullName))
//                    {
//                        stack.Push(Assembly.Load(reference));
//                        list.Add(reference.FullName);
//                    }

//            }
//            while (stack.Count > 0);

//        }

//        #endregion
//    }
//}

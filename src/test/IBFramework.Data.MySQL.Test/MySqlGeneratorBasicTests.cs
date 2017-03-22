using IBFramework.Core.Data.SQL;
using IBFramework.IoC;
using IBFramework.TestHelper;
using IBFramework.TestHelper.TestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace IBFramework.Data.MySQL.Test
{
    public class MySqlGeneratorBasicTests : MySqlTestBase
    {
        #region Variables & Constants

        private ISqlPropertyGenerator _propertyGenerator;

        #endregion

        #region Constructor

        public MySqlGeneratorBasicTests()
        {
            _propertyGenerator = ServiceLocator.Instance.Resolve<ISqlPropertyGenerator>();
        }

        #endregion

        #region Tests

        #region GenerateGetQuery

        [Fact]
        public void GenerateGetQuery_Works_As_Expected()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery();

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM ChildEntity `THIS`;";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateGetQuery_Works_As_Expected_With_Where_Clause()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery(null, "WHERE Id = @id");

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM ChildEntity `THIS` WHERE Id = @id;";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateGetQuery_Works_As_Expected_With_Limit()
        {
            const int limit = 5;

            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery(null, null, limit);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM ChildEntity `THIS` LIMIT {limit};";

            Assert.Equal(expected, result);
        }


        [Fact]
        public void GenerateGetQuery_Works_As_Expected_With_Limit_And_Offset()
        {
            const int limit = 5;
            const int offset = 10;

            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery(null, null, limit, offset);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM ChildEntity `THIS` LIMIT {limit} OFFSET {offset};";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateGetQuery_Offset_Without_Limit_Throws_Exception()
        {
            const int offset = 10;

            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var e = Assert.Throws<Exception>(() => _sut.GenerateGetQuery(null, null, null, offset));

            Assert.Equal("Unable to use a limit without an offset", e.Message);
        }

        [Fact]
        public void GenerateGetQuery_Works_As_Expected_With_Join_Clause()
        {
            const string joinClause = "JOIN CoreEntity ON CoreEntity.Id = ChildEntity.CoreEntityId";

            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery(null, joinClause);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                            Select(x => $"`THIS`.{x}").
                            Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM ChildEntity `THIS` {joinClause};";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateGetQuery_Works_As_Expected_With_All_Params()
        {
            const string whereClause = "WHERE Id = @Id";
            const string joinClause = "JOIN CoreEntity ON CoreEntity.Id = ChildEntity.CoreEntityId";
            const int limit = 5;
            const int offset = 10;

            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery(whereClause, joinClause, limit, offset);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                Select(x => $"`THIS`.{x}").
                Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM ChildEntity `THIS` {joinClause} {whereClause} LIMIT {limit} OFFSET {offset};";

            Assert.Equal(expected, result);
        }

        #endregion

        #region GenerateDeleteQuery

        [Fact]
        public void GenerateDeleteAllQuery_Works_As_Expected()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateDeleteQuery();

            string expected = $"DELETE FROM ChildEntity;";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateDeleteAllQuery_Works_As_Expected_With_Where_Clause()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateDeleteQuery("WHERE `Id` = @id");

            string expected = $"DELETE FROM ChildEntity WHERE `Id` = @id;";

            Assert.Equal(expected, result);
        }

        #endregion

        #region GenerateInsertQuery

        [Fact]
        public void GenerateInsertQuery_Generates_Query_As_Expected()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateInsertQuery("(`Name`)", "('test')");

            string expected = $"INSERT INTO ChildEntity (`Name`) VALUES ('test');";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateInsertQuery_Prepends_Parenthesis_Sql_Value_If_Necessary()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateInsertQuery("`Name`)", "('test')");

            string expected = $"INSERT INTO ChildEntity (`Name`) VALUES ('test');";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateInsertQuery_Appends_Parenthesis_To_Sql_Value_If_Necessary()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateInsertQuery("(`Name`", "('test')");

            string expected = $"INSERT INTO ChildEntity (`Name`) VALUES ('test');";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateInsertQuery_Prepends_Parenthesis_To_InsertClause_As_Necessary()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateInsertQuery("(`Name`)", "'test')");

            string expected = $"INSERT INTO ChildEntity (`Name`) VALUES ('test');";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateInsertQuery_Apppends_Parenthesis_To_InsertClause_As_Necessary()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateInsertQuery("(`Name`)", "('test'");

            string expected = $"INSERT INTO ChildEntity (`Name`) VALUES ('test');";

            Assert.Equal(expected, result);
        }

        #endregion

        #region GenerateInsertQuery (Generic)

        [Fact]
        public void GenerateInsertQuery_Generates_Query_As_Expected_For_Entity()
        {
            ISqlGenerator<BlobEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<BlobEntity>>();

            var entity = new BlobEntity().GenerateForTest();

            Dictionary<string, object> parms = new Dictionary<string, object>();

            var result = _sut.GenerateInsertQuery(entity, ref parms);

            string expected = $"INSERT INTO BlobEntity (`Name`, `Integer`, `Decimal`, `Double`) VALUES (@Name0, @Integer0, @Decimal0, @Double0);";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateInsertQuery_Configures_Params_Appropriately()
        {
            ISqlGenerator<BlobEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<BlobEntity>>();

            var entity = new BlobEntity().GenerateForTest();

            Dictionary<string, object> parms = new Dictionary<string, object>();

            var result = _sut.GenerateInsertQuery(entity, ref parms);

            var entityProps = entity.GetType().GetProperties();

            foreach (var prop in entityProps)
            {
                var matchingVal = parms[$"{prop.Name}0"];
                var propVal = prop.GetValue(entity);

                Assert.Equal(propVal, matchingVal);
            }
        }

        #endregion

        #region GenerateInsertQuery (Generic Enumerable)

        [Fact]
        public void GenerateInsertQuery_With_Generic_Enumerable_Generates_Query_As_Expected_For_Entity()
        {
            ISqlGenerator<BlobEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<BlobEntity>>();

            var entities = Enumerable.Range(0, 3)
                .Select(x => new BlobEntity().GenerateForTest());

            Dictionary<string, object> parms = new Dictionary<string, object>();

            var result = _sut.GenerateInsertQuery(entities, ref parms);

            string expected = $"INSERT INTO BlobEntity (`Name`, `Integer`, `Decimal`, `Double`) VALUES (@Name0, @Integer0, @Decimal0, @Double0), (@Name1, @Integer1, @Decimal1, @Double1), (@Name2, @Integer2, @Decimal2, @Double2);";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateInsertQuery_With_Generic_Enumerable_Configures_Params_Appropriately()
        {
            ISqlGenerator<BlobEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<BlobEntity>>();

            var entities = Enumerable.Range(0, 3)
                .Select(x => new BlobEntity().GenerateForTest())
                .ToList();

            Dictionary<string, object> parms = new Dictionary<string, object>();

            var result = _sut.GenerateInsertQuery(entities, ref parms);

            var entityProps = entities[0].GetType().GetProperties();

            for (var x = 0; x < entities.Count; x++)
            {
                foreach (var prop in entityProps)
                {
                    var matchingVal = parms[$"{prop.Name}{x}"];
                    var propVal = prop.GetValue(entities[x]);

                    Assert.Equal(propVal, matchingVal);
                }
            }
        }

        #endregion

        #region GenerateUpdateQuery

        [Fact]
        public void GenerateUpdateQuery_Formats_Query_As_Expected()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateUpdateQuery("`Name` = 'test'");

            const string expected = "UPDATE ChildEntity SET `Name` = 'test';";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateUpdateQuery_Appends_Where_If_Provided()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.Resolve<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateUpdateQuery("`Name` = 'test'", "WHERE `Id` = @id");

            const string expected = "UPDATE ChildEntity SET `Name` = 'test' WHERE `Id` = @id;";

            Assert.Equal(expected, result);
        }

        #endregion

        #endregion
    }
}

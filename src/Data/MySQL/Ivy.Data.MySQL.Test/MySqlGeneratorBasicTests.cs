using Ivy.Data.Core.Interfaces.SQL;
using Ivy.IoC;
using Ivy.TestHelper;
using Ivy.TestHelper.TestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Ivy.Data.MySQL.Test
{
    public class MySqlGeneratorBasicTests : MySqlTestBase
    {
        #region Variables & Constants

        private ISqlPropertyGenerator _propertyGenerator;

        #endregion

        #region Constructor

        public MySqlGeneratorBasicTests()
        {
            _propertyGenerator = ServiceLocator.Instance.GetService<ISqlPropertyGenerator>();
        }

        #endregion

        #region Tests

        #region GenerateGetCountQuery

        [Fact]
        public void GenerateGetCountQuery_Returns_As_Expected()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetCountQuery();

            Assert.Equal($"SELECT COUNT(*) FROM childentity `THIS`;", result);
        }

        [Fact]
        public void GenerateGetCountQuery_Returns_As_Expected_With_SqlJoin()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            const string sqlJoin = "JOIN CoreEntity CE ON (`THIS`.`CoreEntityId` = `CE`.`Id`)";

            var result = _sut.GenerateGetCountQuery(null, sqlJoin);

            Assert.Equal($"SELECT COUNT(*) FROM childentity `THIS` {sqlJoin};", result);
        }

        [Fact]
        public void GenerateGetCountQuery_Returns_As_Expected_With_SqlWhere()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            const string sqlWhere = "WHERE `THIS`.`CoreEntityId` = @coreEntityId";

            var result = _sut.GenerateGetCountQuery(sqlWhere);

            Assert.Equal($"SELECT COUNT(*) FROM childentity `THIS` {sqlWhere};", result);
        }

        [Fact]
        public void GenerateGetCountQuery_Returns_As_Expected_With_SqlJoin_And_SqlWhere()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            const string sqlJoin = "JOIN CoreEntity CE ON (`THIS`.`CoreEntityId` = `CE`.`Id`)";
            const string sqlWhere = "WHERE `THIS`.`CoreEntityId` = @coreEntityId";

            var result = _sut.GenerateGetCountQuery(sqlWhere, sqlJoin);

            var expected = $"SELECT COUNT(*) FROM childentity `THIS` {sqlJoin} {sqlWhere};";

            Assert.Equal(expected, result);
        }

        #endregion

        #region GenerateGetQuery

        [Fact]
        public void GenerateGetQuery_Works_As_Expected()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery();

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM childentity `THIS`;";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateGetQuery_Works_As_Expected_With_Select_Prefix()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery("DISTINCT");

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT DISTINCT {expectedAttrString} FROM childentity `THIS`;";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateGetQuery_Works_As_Expected_With_Where_Clause()
        {
            const string where = "WHERE Id = @id";

            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery(null, null, where);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM childentity `THIS` {where};";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateGetQuery_Works_As_Expected_With_OrderBy_Clause()
        {
            const string orderBy = "ORDER BY `Id` DESC";

            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery(null, null, null, orderBy);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM childentity `THIS` {orderBy};";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateGetQuery_Works_As_Expected_With_Limit()
        {
            const int limit = 5;

            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery(null, null, null, null, limit);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM childentity `THIS` LIMIT {limit};";

            Assert.Equal(expected, result);
        }


        [Fact]
        public void GenerateGetQuery_Works_As_Expected_With_Limit_And_Offset()
        {
            const int limit = 5;
            const int offset = 10;

            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery(null, null, null, null, limit, offset);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                                        Select(x => $"`THIS`.{x}").
                                        Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM childentity `THIS` LIMIT {limit} OFFSET {offset};";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateGetQuery_Offset_Without_Limit_Throws_Exception()
        {
            const int offset = 10;

            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var e = Assert.Throws<Exception>(() => _sut.GenerateGetQuery(null, null, null, null, null, offset));

            Assert.Equal("Unable to use a limit without an offset", e.Message);
        }

        [Fact]
        public void GenerateGetQuery_Works_As_Expected_With_Join_Clause()
        {
            const string joinClause = "JOIN CoreEntity ON CoreEntity.Id = ChildEntity.CoreEntityId";

            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery(null, null, joinClause);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                            Select(x => $"`THIS`.{x}").
                            Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM childentity `THIS` {joinClause};";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateGetQuery_Works_As_Expected_With_All_Params()
        {
            const string whereClause = "WHERE Id = @Id";
            const string joinClause = "JOIN coreentity ON CoreEntity.Id = childentity.CoreEntityId";
            const string orderClause = "ORDER BY `Id` DESC";
            const int limit = 5;
            const int offset = 10;

            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateGetQuery(null, whereClause, joinClause, orderClause, limit, offset);

            var attrs = _propertyGenerator.GetSqlPropertyNames<ChildEntity>().Select(FormatSqlAttr);

            var expectedAttrString = attrs.
                Select(x => $"`THIS`.{x}").
                Aggregate((x, y) => x + $", {y}");

            string expected = $"SELECT {expectedAttrString} FROM childentity `THIS` {joinClause} {whereClause} {orderClause} LIMIT {limit} OFFSET {offset};";

            Assert.Equal(expected, result);
        }

        #endregion

        #region GenerateDeleteQuery

        [Fact]
        public void GenerateDeleteAllQuery_Works_As_Expected()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateDeleteQuery();

            string expected = $"DELETE FROM childentity;";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateDeleteAllQuery_Works_As_Expected_With_Where_Clause()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateDeleteQuery("WHERE `Id` = @id");

            string expected = $"DELETE FROM childentity WHERE `Id` = @id;";

            Assert.Equal(expected, result);
        }

        #endregion

        #region GenerateInsertQuery

        [Fact]
        public void GenerateInsertQuery_Generates_Query_As_Expected()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateInsertQuery("(`Name`)", "('test')");

            string expected = $"INSERT INTO childentity (`Name`) VALUES ('test');";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateInsertQuery_Prepends_Parenthesis_Sql_Value_If_Necessary()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateInsertQuery("`Name`)", "('test')");

            string expected = $"INSERT INTO childentity (`Name`) VALUES ('test');";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateInsertQuery_Appends_Parenthesis_To_Sql_Value_If_Necessary()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateInsertQuery("(`Name`", "('test')");

            string expected = $"INSERT INTO childentity (`Name`) VALUES ('test');";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateInsertQuery_Prepends_Parenthesis_To_InsertClause_As_Necessary()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateInsertQuery("(`Name`)", "'test')");

            string expected = $"INSERT INTO childentity (`Name`) VALUES ('test');";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateInsertQuery_Apppends_Parenthesis_To_InsertClause_As_Necessary()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateInsertQuery("(`Name`)", "('test'");

            string expected = $"INSERT INTO childentity (`Name`) VALUES ('test');";

            Assert.Equal(expected, result);
        }

        #endregion

        #region GenerateInsertQuery (Generic)

        [Fact]
        public void GenerateInsertQuery_Generates_Query_As_Expected_For_Entity()
        {
            ISqlGenerator<BlobEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<BlobEntity>>();

            var entity = new BlobEntity().GenerateForTest();

            Dictionary<string, object> parms = new Dictionary<string, object>();

            var result = _sut.GenerateInsertQuery(entity, ref parms);

            string expected = $"INSERT INTO blobentity (`Name`, `Integer`, `Decimal`, `Double`, `Boolean`) VALUES (@Name0, @Integer0, @Decimal0, @Double0, @Boolean0);";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateInsertQuery_Configures_Params_Appropriately()
        {
            ISqlGenerator<BlobEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<BlobEntity>>();

            var entity = new BlobEntity().GenerateForTest();

            Dictionary<string, object> parms = new Dictionary<string, object>();

            var result = _sut.GenerateInsertQuery(entity, ref parms);

            var entityProps = entity.GetType().GetProperties().Where(x => x.Name != "References");

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
            ISqlGenerator<BlobEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<BlobEntity>>();

            var entities = Enumerable.Range(0, 3)
                .Select(x => new BlobEntity().GenerateForTest());

            Dictionary<string, object> parms = new Dictionary<string, object>();

            var result = _sut.GenerateInsertQuery(entities, ref parms);

            string expected = $"INSERT INTO blobentity (`Name`, `Integer`, `Decimal`, `Double`, `Boolean`) VALUES (@Name0, @Integer0, @Decimal0, @Double0, @Boolean0), (@Name1, @Integer1, @Decimal1, @Double1, @Boolean1), (@Name2, @Integer2, @Decimal2, @Double2, @Boolean2);";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateInsertQuery_With_Generic_Enumerable_Configures_Params_Appropriately()
        {
            ISqlGenerator<BlobEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<BlobEntity>>();

            var entities = Enumerable.Range(0, 3)
                .Select(x => new BlobEntity().GenerateForTest())
                .ToList();

            Dictionary<string, object> parms = new Dictionary<string, object>();

            var result = _sut.GenerateInsertQuery(entities, ref parms);

            var entityProps = entities[0].GetType().GetProperties().Where(x => x.Name != "References");

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
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateUpdateQuery("SET `Name` = 'test'");

            const string expected = "UPDATE childentity SET `Name` = 'test';";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateUpdateQuery_Appends_Where_If_Provided()
        {
            ISqlGenerator<ChildEntity> _sut = ServiceLocator.Instance.GetService<ISqlGenerator<ChildEntity>>();

            var result = _sut.GenerateUpdateQuery("SET `Name` = 'test'", "WHERE `Id` = @id");

            const string expected = "UPDATE childentity SET `Name` = 'test' WHERE `Id` = @id;";

            Assert.Equal(expected, result);
        }

        #endregion

        #endregion
    }
}

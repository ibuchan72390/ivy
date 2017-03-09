using IBFramework.Core.Data.Domain;
using IBFramework.Core.Data.SQL;

namespace IBFramework.Data.MySQL
{
    public class MySqlGenerator<TEntity> : ISqlGenerator<TEntity>
	{
	    public string GenerateGetAllQuery()
	    {
		    throw new System.NotImplementedException();
	    }

	    public string GenerateDeleteAllQuery()
	    {
		    throw new System.NotImplementedException();
	    }
	}


	public class MySqlGenerator<TEntity, TKey> : ISqlGenerator<TEntity, TKey> 
		where TEntity : IEntityWithTypedId<TKey>
	{
		public string GenerateGetAllQuery()
		{
			throw new System.NotImplementedException();
		}

		public string GenerateDeleteAllQuery()
		{
			throw new System.NotImplementedException();
		}

		public string GenerateDeleteQuery(TKey idToDelete)
		{
			throw new System.NotImplementedException();
		}

		public string GenerateGetQuery(TKey idToGet)
		{
			throw new System.NotImplementedException();
		}

		public string GenerateSaveOrUpdateQuery(TEntity entity)
		{
			throw new System.NotImplementedException();
		}
	}
}

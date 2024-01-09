using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using ToDo.Application.Interfaces.Repositories;
using ToDo.Domain.Attributes;
using ToDo.Domain.Bases;
using ToDo.Infrastructure.Configurations;

namespace ToDo.Infrastructure.Data.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T : BaseEntity
	{
		private readonly IMongoCollection<T> _collection;
		private readonly ILogger<MongoRepository<T>> _logger;

		public MongoRepository(MongoDbConfiguration configuration, ILogger<MongoRepository<T>> logger)
		{
			_logger = logger;
			try
			{
				var database = new MongoClient(configuration.ConnectionString).GetDatabase(configuration.DatabaseName);
				_collection = database.GetCollection<T>(GetCollectionName());
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao se conectar no mongo db: {ExMessage}", ex.Message);
				throw;
			}
			
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await _collection.Find(_ => true).ToListAsync();
		}

		public async Task<T> GetById(string id)
		{
			var objectId = new ObjectId(id);
			return await _collection.Find(entity => objectId.Equals(entity.Id)).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> filter)
		{
			return await _collection.Find(filter).ToListAsync();
		}

		public async Task<T> Add(T entity)
		{
			await _collection.InsertOneAsync(entity);
			return entity;
		}

		public async Task<bool> Update(T entity)
		{
			var dbEntity = await GetById(entity.Id.ToString());
			entity.CreatedAt = dbEntity.CreatedAt;

			var result = await _collection.ReplaceOneAsync(e => entity.Id.Equals(e.Id), entity);
			return result.IsAcknowledged && result.ModifiedCount > 0;
		}

		public async Task<bool> Remove(string id)
		{
			var objectId = new ObjectId(id);
			var result = await _collection.DeleteOneAsync(e => objectId.Equals(e.Id));
			return result.IsAcknowledged && result.DeletedCount > 0;
		}


		private string? GetCollectionName()
		{
			var entityType = typeof(T);
			return (entityType.GetCustomAttributes(typeof(BsonCollectionAttribute), true)
						.FirstOrDefault() as BsonCollectionAttribute)?.CollectionName;
		}
	}
}

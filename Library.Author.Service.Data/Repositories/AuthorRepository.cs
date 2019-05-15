using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Library.Author.Service.Data.Entities;
using Library.Common.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Library.Author.Service.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class AuthorRepository : IAuthorRepository
    {
        private const string CollectionName = "Author";
        private readonly IMongoDatabase _database;

        public AuthorRepository(IMongoClient client)
        {
            _database = client.GetDatabase("LibraryOS");
        }

        public void Delete(string id)
        {
            var collection = _database.GetCollection<EAuthor>(CollectionName);

            collection.DeleteOneAsync(x => x.Id.Equals(id));
        }

        public void Update(EAuthor entity)
        {
            var collection = _database.GetCollection<EAuthor>(CollectionName);

            collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity, new UpdateOptions
                                                                            {
                                                                                IsUpsert = true
                                                                            });
        }

        public void Insert(EAuthor entity)
        {
            var collection = _database.GetCollection<EAuthor>(CollectionName);

            collection.InsertOne(entity);
        }

        public IEnumerable<EAuthor> SelectAll()
        {
            return _database.GetCollection<EAuthor>(CollectionName).Find(new BsonDocument()).ToListAsync()
                .Result;
        }

        [Obsolete]
        public EAuthor Select(string id)
        {
            return _database.GetCollection<EAuthor>(CollectionName).Find(x => x.Id.Equals(id))
                .FirstOrDefaultAsync().Result;
        }

        public EAuthor Select(string key, SearchType searchType)
        {
            EAuthor author;
            switch (searchType)
            {
                case SearchType.ID:
                    author = _database.GetCollection<EAuthor>(CollectionName).Find(x => x.Id.Equals(key))
                        .FirstOrDefaultAsync().Result;
                    break;
                case SearchType.TEXT:
                    author = _database.GetCollection<EAuthor>(CollectionName).Find(x => x.Name.Contains(key))
                        .FirstOrDefaultAsync().Result;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(searchType), searchType, null);
            }

            return author;
        }
    }
}
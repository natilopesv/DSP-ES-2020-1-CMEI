using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace _1_Repository
{
    public class MongoRepository
    {
        public static readonly ConnectionConfig DefaultConfig = new ConnectionConfig(
            "cluster0.p4j0z.mongodb.net",
            "mongo",
            "Ib6CZGOhdbdzqoWT",
            "es-cmei"
        );

        private readonly IMongoDatabase mongoDatabase;
        private readonly string collectionName;

        public MongoRepository(ConnectionConfig config, string collectionName)
        {
            this.collectionName = collectionName;
            var mongoClient = CreateClient(config);
            mongoDatabase = mongoClient.GetDatabase("es-cmei");
        }

        private static MongoClient CreateClient(ConnectionConfig config)
        {
            return new MongoClient(
                $"mongodb+srv://{config.username}:{config.password}@{config.host}/{config.databaseName}?retryWrites=true&w=majority");
        }

        public byte[] DownloadGridFsFile(string fileId)
        {
            var bucket = new GridFSBucket(mongoDatabase);
            var id = new ObjectId(fileId);
            var file = bucket.DownloadAsBytes(id);
            return file;
        }

        public ObjectId UploadGridFsFile(string fileName, byte[] content)
        {
            var bucket = new GridFSBucket(mongoDatabase);
            return bucket.UploadFromBytes(fileName, content);
        }

        public List<BsonDocument> ListAllDocuments()
        {
            var emptyFilter = new BsonDocument();
            return FindDocuments(emptyFilter);
        }

        public BsonDocument FindDocumentById(ObjectId objectId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
            IFindFluent<BsonDocument, BsonDocument> findDocumentById = FindFluent(filter);
            return findDocumentById.First();
        }

        public void InsertDocument(BsonDocument document)
        {
            var collection = mongoDatabase.GetCollection<BsonDocument>(collectionName);
            collection.InsertOne(document);
        }

        public List<BsonDocument> FindDocuments(FilterDefinition<BsonDocument> filter)
        {
            var findFluent = FindFluent(filter);
            return findFluent.ToList();
        }

        private IFindFluent<BsonDocument, BsonDocument> FindFluent(FilterDefinition<BsonDocument> filter)
        {
            var collection = mongoDatabase.GetCollection<BsonDocument>(collectionName);
            var findFluent = collection.Find(filter);
            return findFluent;
        }
    }
}
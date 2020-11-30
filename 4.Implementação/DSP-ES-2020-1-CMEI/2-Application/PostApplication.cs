using System;
using System.Collections.Generic;
using System.Linq;
using _1_Repository;
using _3_Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _2_Application
{
    public class PostApplication
    {
        private readonly MongoRepository mongoRepository = new MongoRepository(MongoRepository.DefaultConfig, "post");

        public byte[] GetPostImage(string imageId)
        {
            return mongoRepository.DownloadGridFsFile(imageId);
        }

        public Post FindPostById(string id)
        {
            var document = mongoRepository.FindDocumentById(new ObjectId(id));

            return FromBsonDocument(document);
        }

        public List<Post> ListAllPosts()
        {
            var documents = mongoRepository.ListAllDocuments();

            return documents.Select(FromBsonDocument).ToList();
        }

        public List<Post> FindPostsByCmeiName(string cmeiName)
        {
            var filter = Builders<BsonDocument>.Filter.Regex("idCmei", $".*{cmeiName}.*");
            var documents = mongoRepository.FindDocuments(filter);

            return documents.Select(FromBsonDocument).ToList();
        }

        public string UpLoadImage(string fileName, byte[] content)
        {
            return mongoRepository.UploadGridFsFile(fileName, content).ToString();
        }

        public void SavePost(Post post)
        {
            post.createdAt = DateTime.Now;
            post.createdBy = "Professor";
            // post.cmeiName = "061300000324";
            var document = ToBsonDocument(post);
            mongoRepository.InsertDocument(document);
        }

        private static BsonDocument ToBsonDocument(Post post)
        {
            var bsonDocument = new BsonDocument
            {
                {"idCmei", post.cmeiName},
                {"createdAt", post.createdAt},
                {"createdBy", post.createdBy},
                {"title", post.title},
                {"description", post.description},
                {"content", post.content},
            };
            if (post.imageId != null)
            {
                bsonDocument.Add("imageId", post.imageId);
            }

            return bsonDocument;
        }

        private static Post FromBsonDocument(BsonDocument document)
        {
            var post = new Post
            {
                id = document.GetValue("_id").ToString(),
                cmeiName = document.GetValue("idCmei").ToString(),
                createdAt = document.GetValue("createdAt").ToLocalTime(),
                createdBy = document.GetValue("createdBy").ToString(),
                title = document.GetValue("title").ToString(),
                description = document.GetValue("description").ToString(),
                content = document.GetValue("content").ToString(),
            };
            if (document.Contains("imageId"))
            {
                post.imageId = document.GetValue("imageId").ToString();
            }

            return post;
        }
    }
}
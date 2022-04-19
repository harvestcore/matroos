using System;

using Matroos.Backend.Core.Dto;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Matroos.Backend.Core.Tests;

public class Sample : IBaseItem
{
    public static string CollectionName { get; } = "samples";

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("active")]
    public bool Active { get; set; }

    [BsonElement("count")]
    public int Count { get; set; }
}

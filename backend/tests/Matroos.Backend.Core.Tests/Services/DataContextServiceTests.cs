using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Matroos.Backend.Core.Classes;

using MongoDB.Driver;

using Xunit;

namespace Matroos.Backend.Core.Tests.Services;

public class DataContextServiceTests : BaseTest, IDisposable
{
    public DataContextServiceTests() : base() { }

    public async void Dispose()
    {
        await ClearCollection();
        GC.SuppressFinalize(this);
    }

    public async Task ClearCollection()
    {
        IMongoCollection<Sample>? collection = _dataContextService.GetCollection<Sample>();
        await collection.DeleteManyAsync(_ => true);
    }

    [Theory]
    [InlineData("01", true, 1, true)]
    [InlineData("02", false, 2, false)]
    public async void GetItem(string name, bool active, int count, bool shouldFindTheItem)
    {
        // Clear the collection.
        await ClearCollection();

        Sample? sample = new()
        {
            Name = name,
            Active = active,
            Count = count
        };

        // Add the item.
        await _dataContextService.Insert(sample);

        Sample? result = await _dataContextService.Get<Sample>(shouldFindTheItem ? sample.Id : Guid.Empty);

        // Assert the item.
        if (!shouldFindTheItem)
        {
            Assert.Null(result);
            return;
        }
        
        Assert.NotNull(result);
        Assert.Equal(sample.Id, result?.Id);
        Assert.Equal(sample.Name, result?.Name);
        Assert.Equal(sample.Active, result?.Active);
        Assert.Equal(sample.Count, result?.Count);
    }

    [Theory]
    [InlineData("03", true, 1)]
    [InlineData("04", false, 2)]
    public async void InsertItem(string name, bool active, int count)
    {
        // Clear the collection.
        await ClearCollection();

        // Add the item.
        bool result = await _dataContextService.Insert(new Sample()
        {
            Name = name,
            Active = active,
            Count = count
        });

        // Assert the insertion.
        Assert.True(result);
    }

    [Theory]
    [InlineData("05", true, 1)]
    [InlineData("06", false, 2)]
    public async void UpdateItem(string name, bool active, int count)
    {
        // Clear the collection.
        await ClearCollection();

        Sample? sample = new()
        {
            Name = name,
            Active = active,
            Count = count
        };

        // Add the item.
        await _dataContextService.Insert(sample);

        // Modify the item.
        sample.Name = $"{name}{name}";
        sample.Active = !active;
        sample.Count = 0;

        // Update the item.
        bool updateResult = await _dataContextService.Update(sample.Id, sample);

        // Assert the result.
        Assert.True(updateResult);

        // Get the sample.
        sample = await _dataContextService.Get<Sample>(sample.Id);

        // Assert the updated values.
        Assert.Equal($"{name}{name}", sample?.Name);
        Assert.Equal(!active, sample?.Active);
        Assert.Equal(0, sample?.Count);
    }

    [Theory]
    [InlineData("07", true, 1, true)]
    [InlineData("08", false, 2, false)]
    public async void DeleteItem(string name, bool active, int count, bool shouldBeDeleted)
    {
        // Clear the collection.
        await ClearCollection();

        Sample? sample = new()
        {
            Name = name,
            Active = active,
            Count = count
        };

        // Add the item.
        await _dataContextService.Insert(sample);

        // Delete de item based on the ID.
        bool result = await _dataContextService.Delete<Sample>(shouldBeDeleted ? sample.Id : Guid.Empty);

        // Assert the insertion.
        Assert.Equal(shouldBeDeleted, result);
    }

    [Fact]
    public async void GetAll()
    {
        // Clear the collection.
        await ClearCollection();
        int numberOfSamples = 10;

        // Insert 10 Samples.
        foreach (var i in Enumerable.Range(0, numberOfSamples))
        {
            await _dataContextService.Insert(new Sample()
            {
                Name = i.ToString(),
                Active = true,
                Count = i
            });
        }

        // Get all the samples.
        List<Sample> results = await _dataContextService.GetAll<Sample>();

        // Assert the results.
        Assert.NotNull(results);
        Assert.NotEmpty(results);
        Assert.Equal(numberOfSamples, results.Count);
    }

    [Fact]
    public async void GetAllQueryFilter()
    {
        // Clear the collection.
        await ClearCollection();
        int numberOfSamples = 100;

        // Insert 10 Samples.
        foreach (var i in Enumerable.Range(0, numberOfSamples))
        {
            await _dataContextService.Insert(new Sample()
            {
                Name = i.ToString(),
                Active = true,
                Count = i
            });
        }

        // Get the first 10 samples, no search term. (0-9)
        List<Sample> results = await _dataContextService.GetAll<Sample>(new()
        {
            Limit = 10,
        });

        // Assert the results.
        Assert.NotNull(results);
        Assert.NotEmpty(results);
        Assert.Equal(10, results.Count);
        Assert.Equal("0", results.First().Name);
        Assert.Equal("9", results.Last().Name);

        // Get the next 10 samples, no search term. (10-19)
        results = await _dataContextService.GetAll<Sample>(new()
        {
            Limit = 10,
            Skip = 10
        });

        // Assert the results.
        Assert.NotNull(results);
        Assert.NotEmpty(results);
        Assert.Equal(10, results.Count);
        Assert.Equal("10", results.First().Name);
        Assert.Equal("19", results.Last().Name);

        // Get the first 10 samples, with search term ("1") but no fields where to search.
        // No fields where to search. -> The search term is ignoread.
        results = await _dataContextService.GetAll<Sample>(new()
        {
            SearchTerm = "1",
            Limit = 10
        });

        // Assert the results.
        Assert.NotNull(results);
        Assert.NotEmpty(results);
        Assert.Equal(10, results.Count);
        Assert.Equal("0", results.First().Name);
        Assert.Equal("9", results.Last().Name);

        // Get the first 10 samples, with search term ("1") and a field where to search.
        results = await _dataContextService.GetAll<Sample>(new()
        {
            SearchTerm = "1",
            SearchFields = new List<string> { "name" },
            Limit = 10
        });

        // Assert the results.
        Assert.NotNull(results);
        Assert.NotEmpty(results);
        Assert.Equal(10, results.Count);
        Assert.Equal("1", results.First().Name);
        Assert.Equal("18", results.Last().Name);

        // Get the first 10 samples, with search term ("1") and a field where to search, but the field is not a string.
        // The field cannot be searched, so the results must be 0.
        results = await _dataContextService.GetAll<Sample>(new()
        {
            SearchTerm = "1",
            SearchFields = new List<string> { "count" },
            Limit = 10
        });

        // Assert the results.
        Assert.NotNull(results);
        Assert.Empty(results);

        // Set the limit to 0 -> Get all the items.
        results = await _dataContextService.GetAll<Sample>(new()
        {
            Limit = 0
        });

        // Assert the results.
        Assert.NotNull(results);
        Assert.NotEmpty(results);
        Assert.Equal(100, results.Count);
    }
}

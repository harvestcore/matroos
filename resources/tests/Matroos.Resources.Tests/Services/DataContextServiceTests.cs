using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Driver;

using Xunit;

namespace Matroos.Resources.Tests.Services;

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
}
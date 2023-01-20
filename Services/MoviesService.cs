using Dasp5.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AspNetCore.Identity.MongoDbCore.Infrastructure;


namespace Dasp5.Services;

public class MoviesService
{
    private readonly IMongoCollection<Movie> _MoviesCollection;

    public MoviesService(
        IOptions<MongoDbSettings> MovieStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            MovieStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            MovieStoreDatabaseSettings.Value.DatabaseName);

        _MoviesCollection = mongoDatabase.GetCollection<Movie>("Movies");
    }

    public async Task<List<Movie>> GetAsync() =>
        await _MoviesCollection.Find(_ => true).ToListAsync();

    public async Task<Movie?> GetAsync(string id) =>
        await _MoviesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Movie newMovie) =>
        await _MoviesCollection.InsertOneAsync(newMovie);

    public async Task UpdateAsync(string id, Movie updatedMovie) =>
        await _MoviesCollection.ReplaceOneAsync(x => x.Id == id, updatedMovie);

    public async Task RemoveAsync(string id) =>
        await _MoviesCollection.DeleteOneAsync(x => x.Id == id);

    public async Task<List<Movie>> Search(string searchString) =>
        await _MoviesCollection.Find(x => x.Title!.Contains(searchString)).ToListAsync();
    public async Task<List<Movie>> GetByGenreAsync(string movieGenre) =>
        await _MoviesCollection.Find(x => x.Genre == movieGenre).ToListAsync();
    public async Task<List<string>> GetAllGenreAsync() =>
        await _MoviesCollection.Distinct<string>("Genre", FilterDefinition<Movie>.Empty).ToListAsync();
       // await _MoviesCollection.DistinctAsync<string>("Genre",null);
}
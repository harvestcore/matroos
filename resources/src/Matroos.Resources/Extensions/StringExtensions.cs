namespace Matroos.Resources.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Parses the given string to get the workers URLs.
    /// </summary>
    /// <param name="workers">The string to parse.</param>
    /// <returns>A list of strings containing the URLs</returns>
    public static List<string> GetWorkerURLs(this string workers)
    {
        string[] values = workers.Split(";");

        if (values == null || values.Length == 0)
        {
            return new();
        }

        List<string> workerURLs = new(values);
        workerURLs.RemoveAll(w => string.IsNullOrEmpty(w));

        return workerURLs;
    }
}

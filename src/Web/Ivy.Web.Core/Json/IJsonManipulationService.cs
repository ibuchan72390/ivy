namespace Ivy.Web.Core.Json
{
    public interface IJsonManipulationService
    {
        /// <summary>
        /// Takes incoming JSON and removes the attribute of the specified name
        /// </summary>
        /// <param name="json">JSON string representing an object whose attribute you wish to remove</param>
        /// <param name="attributeName">Name of the JSON attribute you wish to remove</param>
        /// <returns>JSON string without the provided attribute name</returns>
        string RemoveJsonAttribute(string json, string attributeName);

        /// <summary>
        /// Takes incoming JSON and casts a single property to the desired output
        /// </summary>
        /// <typeparam name="T">Output cast type</typeparam>
        /// <param name="json">JSON string representing an object whose attribute you wish to extract</param>
        /// <param name="attributeName">Name of the JSON attribute you wish to extract</param>
        /// <returns>A cast JSON property to the desired T Type</returns>
        T ExtractJsonAttribute<T>(string json, string attributeName);
    }
}

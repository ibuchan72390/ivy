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
    }
}

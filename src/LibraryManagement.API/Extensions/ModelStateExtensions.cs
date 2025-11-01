using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibraryManagement.API.Extensions
{
    public static class ModelStateExtensions
    {
        public static IDictionary<string, string[]> GetErrors(this ModelStateDictionary modelState)
        {
            return modelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
        }
    }
}

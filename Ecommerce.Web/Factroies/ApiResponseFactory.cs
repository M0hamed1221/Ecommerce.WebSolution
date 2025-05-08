using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModles;

namespace Ecommerce.Web.Factroies
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiVaildtionResponse(ActionContext context)
        {

            var Erros = context.ModelState.Where(modelStateEntry => modelStateEntry.Value.Errors.Any())
            .Select(modestateentry => new VaildationError()
            {
                Filed = modestateentry.Key,
                Errors = modestateentry.Value.Errors.Select(err => err.ErrorMessage)
            });
            var Reponse = new VaildationErrorModel()
            {
                vaildationErrors = Erros,

            };
            return new BadRequestObjectResult(Reponse);
        }
    }
}

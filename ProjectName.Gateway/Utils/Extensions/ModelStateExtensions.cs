using System.Collections.Generic;
using System.Linq;
using ProjectName.Contract.Error;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProjectName.Gateway.Utils.Extensions {
    public static class ModelStateExtensions {
        public static IEnumerable<ModelStateError> AllErrors (this ModelStateDictionary modelState) {
            var result = new List<ModelStateError> ();
            var errorFields = modelState.Where (ms => ms.Value.Errors.Any ())
                .Select (x => new { x.Key, x.Value.Errors });

            foreach (var errorField in errorFields) {
                string fieldKey = errorField.Key;
                var fieldErrors = errorField.Errors
                    .Select (error => new ModelStateError (fieldKey, error.ErrorMessage));
                result.AddRange (fieldErrors);
            }

            return result;
        }
    }
}
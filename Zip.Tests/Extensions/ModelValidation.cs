using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zip.Tests.Extensions;

public static class ModelValidation
{
    public static IList<ValidationResult> Validate(object model)
    {
        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, ctx, validationResults, true);
        return validationResults;
    }
    
}
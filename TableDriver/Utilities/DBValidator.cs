using System.ComponentModel.DataAnnotations;

namespace TableDriver.Utilities;

public static class DBValidator
{
  public static bool ValidateDBObject(object obj, out List<ValidationResult> ValidationResults)
  {
    ValidationContext validationContext = new ValidationContext(obj);
    ValidationResults = new List<ValidationResult>();
    var result = Validator.TryValidateObject(obj, validationContext, ValidationResults);
    return result;
  }
}

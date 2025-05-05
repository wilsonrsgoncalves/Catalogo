using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Validations;

public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var valor = value as string;

        if (string.IsNullOrEmpty(valor))
        {
            return ValidationResult.Success!;
        }

        if (!char.IsUpper(valor[0]))
        {
            return new ValidationResult("A primeira letra do nome do produto deve ser maiúscula");
        }

        return ValidationResult.Success!;
    }
}

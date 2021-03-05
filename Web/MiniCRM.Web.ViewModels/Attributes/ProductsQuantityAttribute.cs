using System.ComponentModel.DataAnnotations;
using MiniCRM.Web.ViewModels.Products;

namespace MiniCRM.Web.ViewModels.Attributes
{
    public class ProductsQuantityAttribute :
        ValidationAttribute
    {

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var product = (SaleProductCreateModel)validationContext.ObjectInstance;
            var inputQuantity = (int)value;

            if (inputQuantity <= 0)
            {
                return new ValidationResult($"You must add minimum 1 product quantity");
            }

            if (product.Quantity < inputQuantity)
            {
                return new ValidationResult($"You cannot add {inputQuantity} because you have only {product.Quantity}");
            }


            return ValidationResult.Success;
        }


    }
}

using System.ComponentModel.DataAnnotations;
using DevExpert.Marketplace.Business.Models;
using Microsoft.AspNetCore.Http;

namespace DevExpert.Marketplace.Application.ViewModels.InputViewModels;

public class ProductInputViewModel : InputViewModelBase<Product>
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
    [Display(Name = "Nome do Produto")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
    [Display(Name = "Descrição do Produto")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo {0} precisa ter entre {1} e {2} caracteres")]
    [Display(Name = "Preço do Produto")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter entre {1} e {2} caracteres")]
    [Display(Name = "Nome do Produto")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public Guid SellerId { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Display(Name = "Imagens do Produto")]
    public List<IFormFile> Images { get; set; }

    public override Product ToModel()
    {
        return new Product
        {
            Id = Id.GetValueOrDefault(),
            Name = Name,
            Description = Description,
            Price = Price,
            Stock = Stock,
            CategoryId = CategoryId,
            SellerId = SellerId
        };
    }
}
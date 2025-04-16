using System.ComponentModel.DataAnnotations;
using DevExpert.Marketplace.Core.Domain.Base;

namespace DevExpert.Marketplace.Core.Domain.Categories;

public class CategoryInputViewModel : InputViewModelBase
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
    [Display(Name = "Nome da Categoria")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
    [Display(Name = "Descrição da Categoria")]
    public string? Description { get; set; }

    public Category ToModel()
    {
        return new Category
        {
            Id = Id.HasValue ? Id.Value : Guid.NewGuid(),
            Name = Name,
            Description = Description
        };
    }
}
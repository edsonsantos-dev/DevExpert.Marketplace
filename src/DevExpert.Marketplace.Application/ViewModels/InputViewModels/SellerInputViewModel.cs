using System.ComponentModel.DataAnnotations;
using DevExpert.Marketplace.Business.Models;

namespace DevExpert.Marketplace.Application.ViewModels.InputViewModels;

public class SellerInputViewModel : InputViewModelBase<Seller>
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
    [Display(Name = "Nome completo do Vendedor")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
    [Display(Name = "E-mail do Vendedor")]
    public string? Email { get; set; }

    [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
    [Display(Name = "Número de Telefone do Vendedor")]
    public string? PhoneNumber { get; set; }

    public override Seller ToModel()
    {
        return new Seller
        {
            Id = Id.GetValueOrDefault(),
            FullName = FullName,
            Email = Email,
            PhoneNumber = PhoneNumber
        };
    }
}
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Blog.Mvc.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        [Display(Name = "Confirme a senha")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nome de Apresentação")]
        public string FullName { get; set; }
    }
}

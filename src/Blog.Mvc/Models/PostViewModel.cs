using System.ComponentModel.DataAnnotations;

namespace Blog.Mvc.Models
{
    public class PostViewModel
    {
        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }

        public string AuthorName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O campo {0} precisa ter entre {1} e {2} caracteres")]
        [Display(Name = "Título do Post")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1024, MinimumLength = 1, ErrorMessage = "O campo {0} precisa ter entre {1} e {2} caracteres")]
        [Display(Name = "Mensagem")]
        public string Message { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}

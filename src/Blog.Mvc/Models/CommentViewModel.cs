using System.ComponentModel.DataAnnotations;

namespace Blog.Mvc.Models
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public Guid CommentAuthorId { get; set; }

        public string CommentAuthorName { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1024, MinimumLength = 5, ErrorMessage = "O campo {0} precisa ter entre {1} e {2} caracteres")]
        [Display(Name = "Comentário")]
        public string Message { get; set; }
    }
}

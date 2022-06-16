#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LogReg.Models;

[NotMapped]
public class LogUser
{
    [Required(ErrorMessage = " is required!")]
    [EmailAddress]
    public string Email {get;set;}

    [Required(ErrorMessage = " is required!")]
    [MinLength(8, ErrorMessage = " must be at least 8 characters long!")]
    [DataType(DataType.Password)]
    public string Pw {get;set;}
}
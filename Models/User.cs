#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LogReg.Models;

public class User
{
    [Key]
    public int UserId {get;set;}

    [Required(ErrorMessage = " is required!")]
    [MinLength(2, ErrorMessage = " must be at least 2 characters long!")]
    [Display(Name = "First Name")]
    public string FirstName {get;set;}

    [Required(ErrorMessage = " is required!")]
    [MinLength(2, ErrorMessage = " must be at least 2 characters long!")]
    [Display(Name = "Last Name")]
    public string LastName {get;set;}

    [Required(ErrorMessage = " is required!")]
    [EmailAddress]
    public string Email {get;set;}

    [Required(ErrorMessage = " is required!")]
    [MinLength(8, ErrorMessage = " must be at least 8 characters long!")]
    [DataType(DataType.Password)]
    public string Pw {get;set;}

    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = " doesn't match password!")]
    public string ConfirmPw {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

}
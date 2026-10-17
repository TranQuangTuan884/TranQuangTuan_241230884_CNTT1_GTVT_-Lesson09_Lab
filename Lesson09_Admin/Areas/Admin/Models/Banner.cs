using System.ComponentModel.DataAnnotations;
namespace Lesson09_Lab.Areas.Admin.Models;
public class Banner { public int Id {get;set;} [Required, StringLength(100)] public string Name {get;set;} = ""; public int Status {get;set;} = 1; public int Priority {get;set;} = 0; [StringLength(100)] public string? Image {get;set;} [StringLength(350)] public string? Description {get;set;} }
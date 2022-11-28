using System.ComponentModel.DataAnnotations;

namespace ToDo.Web.Mvc.Models;

public class EditItemModel
{
    public Guid Id { get; set; }
    public bool Done { get; set; }
}
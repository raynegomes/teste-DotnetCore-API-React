using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Product
{
  public class ProductDtoUpdate
  {
    [Required(ErrorMessage = "Id do produto é obrigatório")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Nome do produto é obrigatório")]
    [MinLength(2, ErrorMessage = "Nome do produto deve ter no mínimo {1} caracteres")]
    [StringLength(100, ErrorMessage = "Nome do produto de ter o máximo de {1} caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Quantidade do produto em estoque é obrigatório")]
    [Range(0, int.MaxValue, ErrorMessage = "Quantidade de produtos deve ser um número positivo ou igual a 0")]
    public int Amount { get; set; }

    [Required(ErrorMessage = "Valor do produto é obrigatório")]
    [Range(0, float.MaxValue, ErrorMessage = "Valor do produto deve ser um número positivo ou igual a 0")]
    public float UnityPrice { get; set; }
  }
}

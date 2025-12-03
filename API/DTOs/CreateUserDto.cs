using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class CreateUserDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los {1} caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
    [StringLength(20, ErrorMessage = "El teléfono no puede superar los {1} caracteres.")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [StringLength(200, ErrorMessage = "La dirección no puede superar los {1} caracteres.")]
    public string Direction { get; set; } = string.Empty;

    [Required(ErrorMessage = "El país es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "CountryId debe ser un número positivo.")]
    public int CountryId { get; set; }

    [Required(ErrorMessage = "El departamento es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "DepartmentId debe ser un número positivo.")]
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "El municipio es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "MunicipalityId debe ser un número positivo.")]
    public int MunicipalityId { get; set; }
}
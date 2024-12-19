using FluentValidation;
using PedidosApi.Dominio.Dtos;

namespace PedidosApi.Aplicacion.Validadores
{
    public class ClienteDtoValidator : AbstractValidator<ClienteDto>
    {
        public ClienteDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress()
                .WithMessage("El correo electrónico no es válido.");
        }
    }
}

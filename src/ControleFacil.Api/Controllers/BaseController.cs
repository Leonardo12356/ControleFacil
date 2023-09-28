using System.Security.Claims;
using ControleFacil.Api.Contract;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected long _idUsuario;
        protected long ObterIdUsuarioLogado()
        {
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            _ = long.TryParse(id, out long idUsuario);

            return idUsuario;
        }

        protected static ModelErrorContract RetornarModelBadRequest(Exception ex)
        {
            return new ModelErrorContract
            {
                StatusCode = 400,
                Title = "Bad Request",
                Message = ex.Message,
                DateTime = DateTime.Now
            };
        }

        protected static ModelErrorContract RetornarModelNotFound(Exception ex)
        {
            return new ModelErrorContract
            {
                StatusCode = 404,
                Title = "Not Found",
                Message = ex.Message,
                DateTime = DateTime.Now
            };
        }

        protected static ModelErrorContract RetornarModelUnauthorized(Exception ex)
        {
            return new ModelErrorContract
            {
                StatusCode = 401,
                Title = "Unauthorized",
                Message = ex.Message,
                DateTime = DateTime.Now
            };
        }


    }
}
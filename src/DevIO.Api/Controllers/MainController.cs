using DevIO.Business.Intefaces;
using DevIO.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace DevIO.Api.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        private readonly INotificador _notificador;

        protected MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
        protected ActionResult CustomResponse(object result = null)
        {
            if(OperacaoValida())
            {
                return Ok(new 
                { 
                    success = true, 
                    data = result 
                });
            }

            return BadRequest(new 
            { 
                success = false, 
                errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }
        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var erroMessage = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(erroMessage);
            }  
        }

        protected void NotificarErro(string erroMessage)
        {
            _notificador.Handle(new Notificacao(erroMessage));
        }



        // Validação de notificações de erro
        // Validação de model state
        //validação de operação de negócios     


    }    


}

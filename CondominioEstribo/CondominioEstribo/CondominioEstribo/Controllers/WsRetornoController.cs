using CondominioEstribo.Models;
using CondominioEstribo.WS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CondominioEstribo.Controllers
{
    public class WsRetornoController : ApiController
    {
        RetornoCliente ws = new RetornoCliente();

        public IHttpActionResult Get()
        {
            var resultList = new  DadosRetorno<List<EntidadeRetornaPessoa>>();

            string mensagem = string.Empty;
            resultList.mensagem = "OK";
            resultList.conteudo = ws.RetornaPessoa();
            resultList.sucesso = true;






            return Ok(resultList);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(HttpRequestMessage request)
        {

            return Ok();
        }


    }
}
using System.Runtime.Serialization;

namespace Mottu.Application.Responses
{
    /// <summary>
    /// Classe genérica usada para montar os
    /// responses das requisições
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract(Name = "return")]
    public class ResponseFactory<T>
    {
        /// <summary>
        /// Inidicador booleano de sucesso ou não da resposta
        /// </summary>
        [DataMember(Name = "success")]
        public bool Sucess { get; private set; }

        /// <summary>
        /// Mensagem de retorno
        /// </summary>
        [DataMember(Name = "message")]
        public string? Message { get; private set; }

        /// <summary>
        /// Conteudo de resposta composto por uma classe com as propriedades preenchidas
        /// que deverão ser retornadas ao requisitante
        /// </summary>
        [DataMember(Name = "content")]
        public T? Content { get; private set; }

        /// <summary>
        /// Métodos que criam o objeto de retorno
        /// </summary>
        /// <returns></returns>
        public static ResponseFactory<T> Success(string message, T content)
        {
            return new ResponseFactory<T>()
            {
                Message = message,
                Content = content,
                Sucess = true
            };
        }

        /// <summary>
        /// Métodos que criam o objeto de retorno para erros
        /// </summary>
        /// <returns></returns>
        public static ResponseFactory<T> Error(string message)
        {
            return new ResponseFactory<T>()
            {
                Message = message,
                Sucess = false
            };
        }
    }
}

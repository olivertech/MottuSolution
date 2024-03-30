namespace Mottu.Application.Services
{
    /// <summary>
    /// Classe genérica usada para montar os
    /// services responses das classes de 
    /// serviços da camada de aplicação
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract(Name = "return")]
    public class ServiceResponseFactory<T>
    {
        /// <summary>
        /// Inidicador booleano de sucesso ou não da resposta
        /// </summary>
        [DataMember(Name = "result")]
        public bool Result { get; private set; }

        /// <summary>
        /// Inidicador do StatusCode de retorno do serviço
        /// </summary>
        [DataMember(Name = "status_code")]
        public EnumStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Mensagem de retorno do serviço
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
        /// Método que criam o objeto de retorno
        /// </summary>
        /// <returns></returns>
        public static ServiceResponseFactory<T> Return(bool result, EnumStatusCode statusCode, string message, T content)
        {
            return new ServiceResponseFactory<T>()
            {
                Result = result,
                StatusCode = statusCode,
                Message = message,
                Content = content
            };
        }

        /// <summary>
        /// Método que criam o objeto de retorno
        /// </summary>
        /// <returns></returns>
        public static ServiceResponseFactory<T> Return(bool result, EnumStatusCode statusCode, string message)
        {
            return new ServiceResponseFactory<T>()
            {
                Result = result,
                StatusCode = statusCode,
                Message = message
            };
        }
    }
}

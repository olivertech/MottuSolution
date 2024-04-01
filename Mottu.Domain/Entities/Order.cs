namespace Mottu.Domain.Entities
{
    public sealed class Order : BaseEntity
    {
        #region Propriedades

        public string? Description { get; set; }
        public DateOnly DateOrder { get; set; }
        public double ValueOrder { get; set; }

        //Navigation Property
        public StatusOrder? StatusOrder { get; set; }

        // Many-to-many relation
        public IList<AcceptedOrder>? AcceptedOrders { get; set; }
        public IList<DeliveredOrder>? DeliveredOrders { get; set; }

        #endregion

        #region Construtores

        public Order()
        {
        }

        public Order(string description, StatusOrder statusOrder, DateOnly dateOrder, double valueOrder)
        {
            Validate(description, statusOrder, dateOrder, valueOrder);
        }

        [JsonConstructor]
        public Order(Guid id, string description, StatusOrder statusOrder, DateOnly dateOrder, double valueOrder)
        {
            DomainValidation.When(id.ToString().Length == 0, "Id inválido");
            Id = id;
            Validate(description, statusOrder, dateOrder, valueOrder);
        }

        #endregion

        #region Validações

        /// TODO: REAVALIAR SE VAI TER COMO USAR ESSAS VALIDAÇÕES
        
        private void Validate(string description, StatusOrder statusOrder, DateOnly dateOrder, double valueOrder)
        {
            DomainValidation.When(statusOrder == null, "Campo 'situação do pedido' é obrigatório");
            
            //TODO: REVER ESSA REGRA DE VALIDAÇÃO
            DomainValidation.When(!statusOrder!.Name!.Contains("disponivel", StringComparison.CurrentCultureIgnoreCase) && !statusOrder!.Name!.Contains("aceito", StringComparison.CurrentCultureIgnoreCase) && !statusOrder!.Name!.Contains("entregue", StringComparison.CurrentCultureIgnoreCase), "Campo 'situação do pedido' é obrigatório");

            DomainValidation.When(dateOrder == DateOnly.MinValue, "Campo 'data do pedido' é obrigatório");
            DomainValidation.When(valueOrder == 0, "Campo 'valor da corrida' é obrigatório");

            //TODO:
            //IMPLEMENTAR AQUI A PESQUISA DA EXISTENCIA OU NÃO DA CNH E DO CNPJ,
            //POIS NÃO PODE EXISTIR MAIS DE UMA CNH E CNPJ NA BASE

            Description = description;
            StatusOrder = statusOrder;
            DateOrder = dateOrder;
            ValueOrder = valueOrder;
        }

        #endregion
    }
}

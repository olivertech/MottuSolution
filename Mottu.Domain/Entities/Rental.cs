namespace Mottu.Domain.Entities
{
    public sealed class Rental : BaseEntity
    {
        #region Propriedades

        public DateOnly CreationDate { get; set; }
        public DateOnly PredictionDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateOnly InitialDate { get; set; }
        public double TotalValue { get; set; }
        public int NumMoreDailys { get; set; }
        public bool IsActive { get; set; }

        //Navigation Property
        public Bike Bike { get; set; } = new Bike();
        public AppUser User { get; set; } = new AppUser();
        public Plan Plan { get; set; } = new Plan();

        #endregion

        #region Construtores

        public Rental()
        {
        }

        public Rental(DateOnly creationDate, DateOnly predictionDate, DateOnly endDate, DateOnly initialDate, double totalValue, int numMoreDailys, bool isActive, Bike bike, AppUser user)
        {
            Validade(creationDate, predictionDate, endDate, initialDate, totalValue, numMoreDailys, isActive, bike, user);
        }

        [JsonConstructor]
        public Rental(Guid id, DateOnly creationDate, DateOnly predictionDate, DateOnly endDate, DateOnly initialDate, double totalValue, int numMoreDailys, bool isActive, Bike bike, AppUser user)
        {
            DomainValidation.When(id.ToString().Length == 0, "Id inválido");
            Id = id;
            Validade(creationDate, predictionDate, endDate, initialDate, totalValue, numMoreDailys, isActive, bike, user);
        }

        #endregion

        #region Validações

        /// TODO: REAVALIAR SE VAI TER COMO USAR ESSAS VALIDAÇÕES
        
        private void Validade(DateOnly creationDate, DateOnly predictionDate, DateOnly endDate, DateOnly initialDate, double totalValue, int numMoreDailys, bool isActive, Bike bike, AppUser user)
        {
            DomainValidation.When(creationDate == DateOnly.MinValue, "Campo 'data de criação da locação' é obrigatório");
            DomainValidation.When(predictionDate == DateOnly.MinValue, "Campo 'data de previsão de término da locação' é obrigatório");
            DomainValidation.When(endDate == DateOnly.MinValue, "Campo 'data de término da locação' é obrigatório");
            DomainValidation.When(initialDate == DateOnly.MinValue, "Campo 'data de criação da locação' é obrigatório");

            CreationDate = creationDate;
            PredictionDate = predictionDate;
            EndDate = endDate;
            InitialDate = initialDate;
            TotalValue = totalValue;
            NumMoreDailys = numMoreDailys;
            IsActive = isActive;
            Bike = bike;
            User = user;
        }

        #endregion
    }
}

namespace Mottu.Domain.Entities
{
    public sealed class Plan : BaseEntity
    {
        #region Propriedades

        public string? Name { get; set; }
        public string? Description { get; set; }
        public int NumDays { get; private set; }
        public double DailyValue { get; private set; }
        public int FinePercentage { get; private set; }

        #endregion

        #region Construtores

        public Plan()
        {
        }

        public Plan(string name, string description, int numDays, double dailyValue, int finePercentage)
        {
            Validate(name, description, numDays, dailyValue, finePercentage);
        }

        [JsonConstructor]
        public Plan(Guid id, string name, string description, int numDays, double dailyValue, int finePercentage)
        {
            DomainValidation.When(id.ToString().Length == 0, "Id inválido");
            Id = id;
            Validate(name, description, numDays, dailyValue, finePercentage);
        }

        #endregion

        #region Validações

        /// TODO: REAVALIAR SE VAI TER COMO USAR ESSAS VALIDAÇÕES
        
        private void Validate(string name, string description, int numDays, double dailyValue, int finePercentage)
        {
            //TODO: IMPLEMENTAR AQUI A RECUPERAÇÃO DESSES VALORES DO BANCO
            int[] days = [7, 15, 30];
            int[] percs = [20, 40, 60];
            double[] values = [30.0, 28.0, 22.0];

            DomainValidation.When(!days.Contains(numDays), "campo 'número de dias' é obrigatório e deve conter apenas 7, 15 ou 30");
            DomainValidation.When(!percs.Contains(finePercentage), "campo 'percentual de multa' é obrigatório e deve conter apenas 20, 40 ou 60");
            DomainValidation.When(!values.Contains(dailyValue), "campo 'valor da diária' é obrigatório e deve conter apenas 30.00, 28.00 ou 22.00");

            Name = name;
            Description = description;
            NumDays = numDays;
            DailyValue = dailyValue;
            FinePercentage = finePercentage;
        }

        #endregion
    }
}

using Mottu.Domain.EntitiesMDB.Base;

namespace Mottu.Domain.EntitiesMDB
{
    public sealed class RentalMDB : BaseEntityMDB
    {
        #region Propriedades

        /// <summary>
        /// Data de criação da locação
        /// </summary>
        public DateOnly CreationDate { get; set; }

        /// <summary>
        /// Data prevista/estimada de fim da locação
        /// </summary>
        public DateOnly PredictionDate { get; set; }

        /// <summary>
        /// Data efetiva/de fato do fim da locação
        /// </summary>
        public DateOnly EndDate { get; set; }

        /// <summary>
        /// Data de início da locação que é sempre 1 dia após a data de criação
        /// </summary>
        public DateOnly InitialDate { get; set; }

        /// <summary>
        /// Valor total da locação a ser calculado no final do período,
        /// incluindo o número dias do plano escolhido, acrescido de
        /// multas quando houver
        /// </summary>
        public double TotalValue { get; set; }

        /// <summary>
        /// Número de dias a mais da locação
        /// </summary>
        public int NumMoreDailys { get; set; }

        /// <summary>
        /// Se a locação está em curso
        /// </summary>
        public bool IsActive { get; set; }

        //Navigation Property
        public Bike Bike { get; set; } = new Bike();
        public AppUser User { get; set; } = new AppUser();
        public Plan Plan { get; set; } = new Plan();

        #endregion
    }
}

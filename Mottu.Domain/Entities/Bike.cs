﻿namespace Mottu.Domain.Entities
{
    public sealed class Bike : BaseEntity
    {
        #region Propriedades

        private string? _model { get; set; }

        public string? Model 
        {
            get
            { 
                return _model;
            }
            set
            { 
                if(value is not null)
                    _model = value.ToUpper();
            }
        }

        private string? _plate{ get; set; }

        public string? Plate
        {
            get
            {
                return _plate;
            }
            set
            {
                if (value is not null)
                    _plate = value.ToUpper();
            }
        }

        public string? Year { get; private set; }
        public bool IsLeased { get; set; }

        #endregion

        #region Construtores

        public Bike()
        {
        }

        public Bike(string model, string plate, string year, bool isLeased)
        {
            Validate(model, plate, year, isLeased);
        }

        [JsonConstructor]
        public Bike(Guid id, string model, string plate, string year, bool isLeased)
        {
            DomainValidation.When(id.ToString().Length == 0, "Id inválido");
            Id = id;
            Validate(model, plate, year, isLeased);
        }

        #endregion

        #region Validações

        /// TODO: REAVALIAR SE VAI TER COMO USAR ESSAS VALIDAÇÕES
        
        private void Validate(string model, string plate, string year, bool isLeased)
        {
            DomainValidation.When(string.IsNullOrEmpty(model), "Campo 'modelo' é obrigatório");
            DomainValidation.When(string.IsNullOrEmpty(plate), "Campo 'placa' é obrigatório");
            DomainValidation.When(year is null || year.Length < 4, error: "Campo 'Ano' é obrigatório e precisa ser com 4 dígitos");

            //TODO:
            //IMPLEMENTAR AQUI A PESQUISA DA EXISTENCIA OU NÃO DA PLACA,
            //POIS NÃO PODE EXISTIR MAIS DE UMA MOTO COM A MESMA PLACA

            Model = model.ToUpper();
            Plate = plate.ToUpper();
            Year = year;
            IsLeased = isLeased;
        }

        #endregion
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mottu.Domain.Interfaces;

namespace Mottu.Api.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork? _unitOfWork;
        protected readonly IMapper? _mapper;
        protected string? _nomeEntidade;

        public BaseController(IUnitOfWork unitOfWork, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}

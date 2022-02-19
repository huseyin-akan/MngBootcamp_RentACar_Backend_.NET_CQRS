using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands.DeleteCar
{
    public class DeleteCarCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, IResult>
        {
            ICarRepository _carRepository;
            IMapper _mapper;
            CarBusinessRules _carBusinessRules;

            public DeleteCarCommandHandler(
                ICarRepository carRepository,
                IMapper mapper,
                CarBusinessRules carBusinessRules)
            {
                _carRepository = carRepository;
                _mapper = mapper;
                _carBusinessRules = carBusinessRules;
            }

            public async Task<IResult> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
            {
                var carToDelete = await this._carRepository.GetAsync(c => c.Id == request.Id);
                if(carToDelete is null) throw new BusinessException(Messages.CarNotFound);

                await _carRepository.DeleteAsync(carToDelete);
                return new SuccessResult("Araç silme işlemi başarılı oldu");
            }
        }
    }
}

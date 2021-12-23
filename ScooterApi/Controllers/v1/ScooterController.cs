using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterApi.Domain.Entities;
using ScooterApi.Models.v1;
using ScooterApi.Service.v1.Command;
using ScooterApi.Service.v1.Query;

namespace ScooterApi.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class ScooterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ScooterController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Action to see all existing data from scooters.
        /// </summary>
        /// <returns>Returns a list of all data from scooters</returns>
        /// <response code="200">Returned if data from scooter were loaded</response>
        /// <response code="400">Returned if data from scooter couldn't be loaded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<Scooter>>> Scooters()
        {
            try
            {
                return await _mediator.Send(new GetScootersQuery());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Action to see all existing data from concrete scooter.
        /// </summary>
        /// <returns>Returns a list of all data from concrete scooter</returns>
        /// <response code="200">Returned if data from concrete scooter were loaded</response>
        /// <response code="400">Returned if data from concrete scooter couldn't be loaded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet ("id:int")]
        public async Task<ActionResult<List<Scooter>>> Scooters(int id)
        {
            try
            {
                return await _mediator.Send(new GetScooterByIdQuery()
                    {Id = id});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to create a new data from scooter in the database.
        /// </summary>
        /// <param name="dataFromScooterModel">Model to create a new data from scooter</param>
        /// <returns>Returns the created data from scooter</returns>
        /// <response code="200">Returned if the data from scooter was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or the data from scooter couldn't be saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Scooter>> Scooter(DataFromScooterModel dataFromScooterModel)
        {
            try
            {
                return await _mediator.Send(new CreateScooterCommand
                {
                    Scooter = _mapper.Map<Scooter>(dataFromScooterModel)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Action to see current address from scooters.
        /// </summary>
        /// <returns>Returns a current address from scooters</returns>
        /// <response code="200">Returned if current address from scooter were loaded</response>
        /// <response code="400">Returned if current address from scooter couldn't be loaded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet ("idScooter:int")]
        public async Task<ActionResult<Domain.Entities.Address.Address>> GetCurrentAddress(int idScooter)
        {
            try
            {
                return await _mediator.Send(new GetCurrentAddressQuery() 
                    {Id = idScooter});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
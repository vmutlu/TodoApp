using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Core.Entities.Concrete;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralSettingController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserOperationClaimService _userOperationClaimService;

        public GeneralSettingController(IOperationClaimService operationClaimService, IUserOperationClaimService userOperationClaimService)
        {
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
        }

        #region Operation Claims

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var operationClaims = await _operationClaimService.GetAllAsync().ConfigureAwait(false);

            if (operationClaims.Success)
                return Ok(operationClaims);

            else
                return BadRequest(operationClaims);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var operationClaims = await _operationClaimService.GetByIdAsync(id).ConfigureAwait(false);

            if (operationClaims.Success)
                return Ok(operationClaims);

            else
                return BadRequest(operationClaims);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] OperationClaim operationClaim)
        {
            var operationClaims = await _operationClaimService.AddAsync(operationClaim).ConfigureAwait(false);

            if (operationClaims.Success)
                return Ok(operationClaims);

            else
                return BadRequest(operationClaims);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] OperationClaim operationClaim)
        {
            var operationClaims = await _operationClaimService.UpdateAsync(operationClaim).ConfigureAwait(false);

            if (operationClaims.Success)
                return Ok(operationClaims);

            else
                return BadRequest(operationClaims);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var operationClaims = await _operationClaimService.DeleteAsync(id).ConfigureAwait(false);

            if (operationClaims.Success)
                return Ok(operationClaims);

            else
                return BadRequest(operationClaims);
        }

        #endregion

        #region User Operation Claims

        [HttpGet("UserOperation/getAll")]
        public async Task<IActionResult> GetAllUserOperation()
        {
            var operationClaims = await _userOperationClaimService.GetAllAsync().ConfigureAwait(false);

            if (operationClaims.Success)
                return Ok(operationClaims);

            else
                return BadRequest(operationClaims);
        }

        [HttpGet("UserOperation/getById/{id}")]
        public async Task<IActionResult> GetByIdUserOperation(int id)
        {
            var operationClaims = await _userOperationClaimService.GetByIdAsync(id).ConfigureAwait(false);

            if (operationClaims.Success)
                return Ok(operationClaims);

            else
                return BadRequest(operationClaims);
        }

        [HttpPost("UserOperation/add")]
        public async Task<IActionResult> AddUserOperation([FromBody] UserOperationClaim operationClaim)
        {
            var operationClaims = await _userOperationClaimService.AddAsync(operationClaim).ConfigureAwait(false);

            if (operationClaims.Success)
                return Ok(operationClaims);

            else
                return BadRequest(operationClaims);
        }

        [HttpPut("UserOperation/update")]
        public async Task<IActionResult> UpdateUserOperation([FromBody] UserOperationClaim operationClaim)
        {
            var operationClaims = await _userOperationClaimService.UpdateAsync(operationClaim).ConfigureAwait(false);

            if (operationClaims.Success)
                return Ok(operationClaims);

            else
                return BadRequest(operationClaims);
        }

        [HttpDelete("UserOperation/delete/{id}")]
        public async Task<IActionResult> DeleteUserOperation(int id)
        {
            var operationClaims = await _userOperationClaimService.DeleteAsync(id).ConfigureAwait(false);

            if (operationClaims.Success)
                return Ok(operationClaims);

            else
                return BadRequest(operationClaims);
        }

        #endregion
    }
}

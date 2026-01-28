using BootcampCLT.Application.Command;
using BootcampCLT.Application.Query;
using MediatR;

using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ProductoController : Controller
{

    private readonly IMediator _mediator;
    private readonly ILogger<ProductoController> _logger;

    public ProductoController(IMediator mediator, ILogger<ProductoController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    /// <summary>
    /// Obtiene el detalle de un producto por su identificador.
    /// </summary>
    /// <param name="id">Identificador del producto.</param>
    /// <returns>Producto encontrado.</returns>
    [HttpGet("v1/api/productos")]
    [ProducesResponseType(typeof(IEnumerable<ProductoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ProductoResponse>>> GetProductos()
    {
        _logger.LogInformation("Obteniendo listado de productos");

        var result = await _mediator.Send(new GetProductosQuery());

        if (result == null || !result.Any())
        {
            _logger.LogWarning("No se ha encontrado ningún producto.");

            return NoContent();

        }

        _logger.LogInformation("Listado de productos obtenido satisfactoriamente.");

        return Ok(result);
    }
    /// <summary>
    /// Obtiene el detalle de un producto por su identificador.
    /// </summary>
    /// <param name="id">Identificador del producto.</param>
    /// <returns>Producto encontrado.</returns>
    [HttpGet("v1/api/productos/{id:int}")]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductoResponse>> GetProductoById([FromRoute] int id)
    {
        _logger.LogInformation("Obteniendo producto con ID {ProductoId}", id);
        var result = await _mediator.Send(new GetProductoByIdQuery(id));

        if (result is null)
        {
            _logger.LogWarning("No se pudo encontrar el producto con ID {ProductId}.", id);

            return NotFound();

        }
        _logger.LogInformation("Producto con ID {ProductoId} obtenido exitosamente", id);

        return Ok(result);
    }
    /// <summary>
    /// Obtiene el detalle de un producto por su identificador.
    /// </summary>
    /// <param name="id">Identificador del producto.</param>
    /// <returns>Producto encontrado.</returns>
    [HttpPost("v1/api/productos")]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductoResponse>> CreateProducto(
    [FromBody] CreateProductoRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _logger.LogInformation("Creando nuevo producto");

        var result = await _mediator.Send(new CreateProductoCommand(request));

        _logger.LogInformation("Producto creado, ID: {ProductId}", result.Id);


        return CreatedAtAction(
            nameof(GetProductoById),
            new { id = result.Id },
            result);
    }

    /// <summary>
    /// Actualiza completamente un producto existente.
    /// </summary>
    /// <param name="id">Identificador del producto.</param>
    /// <param name="request">Datos completos a actualizar.</param>
    [HttpPut("v1/api/productos/{id:int}")]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductoResponse>> UpdateProducto(
        [FromRoute] int id,
        [FromBody] UpdateProductoRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _logger.LogInformation("Actualizando producto con ID {ProductoId}", id);

        var result = await _mediator.Send(new UpdateProductoCommand(id, request));

        if (result is null)
        {
            _logger.LogWarning("No se ha encontrado el producto.");
            return NotFound();

        }
        _logger.LogInformation("Producto con ID {ProductoId} actualizado.", id);

        return Ok(result);
    }

    /// <summary>
    /// Actualiza parcialmente un producto existente.
    /// Solo se modificarán los campos enviados.
    /// </summary>
    /// <param name="id">Identificador del producto.</param>
    /// <param name="request">Campos a actualizar.</param>
    [HttpPatch("v1/api/productos/{id:int}")]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductoResponse>> PatchProducto(
        [FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _logger.LogInformation("Activando producto con ID {ProductoId}", id);

        var result = await _mediator.Send(new PatchProductoCommand(id));

        if (result is null)
        {
            _logger.LogWarning("No se ha encontrado el producto.");

            return NotFound();

        }

        _logger.LogInformation("Producto con ID {ProductoId} activado correctamente", id);

        return Ok(result);
    }

    /// <summary>
    /// Elimina un producto existente.
    /// </summary>
    /// <param name="id">Identificador del producto.</param>
    [HttpDelete("v1/api/productos/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProducto([FromRoute] int id)
    {
        _logger.LogInformation("Eliminando producto con ID {ProductoId}", id);

        var deleted = await _mediator.Send(new DeleteProductoCommand(id));

        if (!deleted)
        {
            _logger.LogWarning("No se ha encontrado el producto.");

            return NotFound();

        }

        _logger.LogInformation("Producto con ID {ProductoId} eliminado", id);

        return NoContent();
    }
}
